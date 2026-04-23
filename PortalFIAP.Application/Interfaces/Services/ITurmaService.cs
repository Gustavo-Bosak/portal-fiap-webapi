using PortalFiap.Domain.Entities;

namespace PortalFIAP.Application.Interfaces;

/// <summary>
/// Contrato do serviço de Turmas. Define as operações que podem ser realizadas com as turmas.
/// </summary>
public interface ITurmaService
{
    /// <summary>
    /// Obtém todas as turmas cadastradas.
    /// </summary>
    /// <returns>Uma coleção de todas as turmas.</returns>
    Task<IEnumerable<Turma>> GetAll();

    /// <summary>
    /// Obtém uma turma específica pelo seu Id.
    /// </summary>
    /// <param name="id">O Id (Guid) da turma a ser buscada.</param>
    /// <returns>A turma correspondente ao Id, ou nula se não for encontrada.</returns>
    Task<Turma?> GetById(Guid id);

    /// <summary>
    /// Obtém todas as turmas de um curso específico.
    /// </summary>
    /// <param name="cursoId">O Id (Guid) do curso para o qual as turmas serão buscadas.</param>
    /// <returns>Uma coleção de turmas do curso especificado.</returns>
    Task<IEnumerable<Turma>> GetByCursoId(Guid cursoId);
}