using PortalFiap.Domain.Entities;

namespace PortalFIAP.Application.Interfaces.Repositories;

public interface ITurmaRepository : IRepository<Turma>
{
    /// <summary>Lista as turmas ativas de um curso.</summary>
    Task<IReadOnlyList<Turma>> GetByCursoIdAsync(Guid cursoId);
}
