using PortalFiap.Domain.Entities;

namespace PortalFIAP.Application.Interfaces.Repositories;

public interface ICursoRepository
{
    Task Add(Curso curso);
    
    Task<Curso?> GetById(Guid id);
    
    Task<IReadOnlyList<Curso>> GetAll();

    Task Update(Curso curso);
    
    Task<bool> Delete(Guid id);
    
    Task SaveChanges();
}