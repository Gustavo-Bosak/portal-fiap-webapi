using Microsoft.EntityFrameworkCore;
using PortalFiap.Domain.Entities;
using PortalFIAP.Application.Interfaces;
using PortalFiap.Infrastructure.Persistence;

namespace PortalFIAP.Application.Services;

public class CursoService : ICursoService
{
    private readonly PortalFiapContext _context;

    public CursoService(PortalFiapContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Curso>> GetAll()
    {
        return await _context.Cursos.ToListAsync();
    }

    public async Task<Curso?> GetById(Guid id)
    {
        return await _context.Cursos.FindAsync(id);
    }
}