using Microsoft.EntityFrameworkCore;
using PortalFIAP.Application.DTO;
using PortalFIAP.Application.Interfaces.Repositories;
using PortalFiap.Domain.Entities;

namespace PortalFiap.Infrastructure.Persistence.Repositories;

public class CursoRepository : ICursoRepository
{
    private readonly PortalFiapContext _context;
    
    public CursoRepository(PortalFiapContext context)
    {
        _context = context;
    }
    
    public async Task Add(Curso request)
    {
        await _context.Cursos.AddAsync(request);
        await _context.SaveChangesAsync();
    }

    public async Task<Curso?> GetById(Guid id) => await _context.Cursos
            .Include(c => c.Turmas)
            .FirstOrDefaultAsync(c => c.Id == id);

    public async Task<IReadOnlyList<Curso>> GetAll() => await _context.Cursos
        .Include(c => c.Turmas)
        .Where(c => c.Active)
        .ToListAsync();


    public async Task Update(Curso curso)
    {
        _context.Update(curso);
        await _context.SaveChangesAsync();
    }
    
    public async Task<bool> Delete(Guid id)
    {
        var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Id == id);
        
        if (curso == null) return false;
        if (!curso.Active) return true;
        
        curso.Deactivate();
        _context.SaveChanges();
        return true;
    }

    public async Task SaveChanges()=> await _context.SaveChangesAsync();
}