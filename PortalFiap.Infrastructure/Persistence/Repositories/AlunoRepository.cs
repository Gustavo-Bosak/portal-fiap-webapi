using Microsoft.EntityFrameworkCore;
using PortalFiap.Domain.Entities;
using PortalFIAP.Application.Interfaces.Repositories;
using PortalFiap.Infrastructure.Persistence;

namespace PortalFiap.Infrastructure.Persistence.Repositories;

public class AlunoRepository : IAlunoRepository
{
    private readonly PortalFiapContext _context;

    public AlunoRepository(PortalFiapContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Aluno>> GetAllAsync()
    {
        return await _context.Alunos
            .Include(a => a.Endereco)
            .Include(a => a.Matriculas).ThenInclude(m => m.Turma)
            .Include(a => a.Matriculas).ThenInclude(m => m.Bolsa)
            .Where(a => a.Active)
            .ToListAsync();
    }

    public async Task<Aluno?> GetByIdAsync(Guid id)
    {
        return await _context.Alunos
            .Include(a => a.Endereco)
            .Include(a => a.Matriculas).ThenInclude(m => m.Turma)
            .Include(a => a.Matriculas).ThenInclude(m => m.Bolsa)
            .Where(a => a.Active)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task AddAsync(Aluno aluno)
    {
        await _context.Alunos.AddAsync(aluno);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Aluno aluno)
    {
        _context.Alunos.Update(aluno);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var aluno = await _context.Alunos.FindAsync(id);
        if (aluno is null) return false;

        aluno.Deactivate();
        await _context.SaveChangesAsync();
        return true;
    }
}
