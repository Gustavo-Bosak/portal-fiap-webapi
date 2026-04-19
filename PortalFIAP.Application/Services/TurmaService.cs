using Microsoft.EntityFrameworkCore;
using PortalFiap.Domain.Entities;
using PortalFIAP.Application.Interfaces;
using PortalFiap.Infrastructure.Persistence;

namespace PortalFIAP.Application.Services;

public class TurmaService : ITurmaService
{
    private readonly PortalFiapContext _context;

    public TurmaService(PortalFiapContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Turma>> GetAll()
    {
        return await _context.Turmas.ToListAsync();
    }

    public async Task<Turma?> GetById(Guid id)
    {
        return await _context.Turmas.FindAsync(id);
    }

    public async Task<IEnumerable<Turma>> GetByCursoId(Guid cursoId)
    {
        return await _context.Turmas.Where(t => t.Curso.Id == cursoId).ToListAsync();
    }
}