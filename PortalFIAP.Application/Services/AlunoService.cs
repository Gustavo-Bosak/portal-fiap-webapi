using Microsoft.EntityFrameworkCore;
using PortalFiap.Domain.Entities;
using PortalFIAP.Application.Interfaces;
using PortalFiap.Infrastructure.Persistence;

namespace PortalFIAP.Application.Services;

public class AlunoService : IAlunoService
{
    private readonly PortalFiapContext _context;

    public AlunoService(PortalFiapContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Aluno>> GetAll()
    {
        return await _context.Alunos.ToListAsync();
    }

    public async Task<Aluno?> GetById(Guid id)
    {
        return await _context.Alunos.FindAsync(id);
    }
}