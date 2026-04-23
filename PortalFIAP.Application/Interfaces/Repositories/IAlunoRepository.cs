using PortalFiap.Domain.Entities;

namespace PortalFIAP.Application.Interfaces.Repositories;

public interface IAlunoRepository
{
    Task<IEnumerable<Aluno>> GetAllAsync();
    Task<Aluno?> GetByIdAsync(Guid id);
    Task AddAsync(Aluno aluno);
    Task UpdateAsync(Aluno aluno);
    Task<bool> DeleteAsync(Guid id);
}
