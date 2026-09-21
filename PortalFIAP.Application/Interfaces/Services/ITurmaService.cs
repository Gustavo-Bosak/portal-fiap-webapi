using PortalFIAP.Application.DTO;

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
    Task<IReadOnlyList<TurmaResponse>> GetAll();

    /// <summary>
    /// Obtém uma turma específica pelo seu Id.
    /// </summary>
    /// <param name="id">O Id (Guid) da turma a ser buscada.</param>
    /// <returns>A turma correspondente ao Id, ou nula se não for encontrada.</returns>
    Task<TurmaResponse?> GetById(Guid id);

    /// <summary>
    /// Obtém todas as turmas de um curso específico.
    /// </summary>
    /// <param name="cursoId">O Id (Guid) do curso para o qual as turmas serão buscadas.</param>
    /// <returns>Uma coleção de turmas do curso especificado.</returns>
    Task<IReadOnlyList<TurmaResponse>> GetByCursoId(Guid cursoId);

    /// <summary>
    /// Cria uma turma vinculada a um curso existente.
    /// </summary>
    /// <exception cref="PortalFiap.Domain.Exceptions.ResourceNotFoundException">Curso não encontrado.</exception>
    Task<TurmaResponse> Create(TurmaRequest request);

    /// <summary>
    /// Atualiza os dados de uma turma existente.
    /// </summary>
    /// <exception cref="PortalFiap.Domain.Exceptions.ResourceNotFoundException">Turma ou curso não encontrado.</exception>
    Task<TurmaResponse> Update(Guid id, TurmaRequest request);

    /// <summary>
    /// Remove (exclusão lógica) uma turma.
    /// </summary>
    /// <returns><c>false</c> se a turma não existir.</returns>
    Task<bool> Delete(Guid id);
}
