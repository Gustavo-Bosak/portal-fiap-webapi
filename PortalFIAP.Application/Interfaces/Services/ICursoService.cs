using PortalFIAP.Application.DTO;
using PortalFiap.Domain.Entities;

namespace PortalFIAP.Application.Interfaces;

/// <summary>
/// Contrato do serviço de Cursos. Define as operações que podem ser realizadas com os cursos.
/// </summary>
public interface ICursoService
{
    Task<CursoResponse> Create(CursoRequest request);
    
    /// <summary>
    /// Obtém todos os cursos cadastrados.
    /// </summary>
    /// <returns>Uma coleção de todos os cursos.</returns>
    Task<IReadOnlyList<CursoResponse>> GetAll();

    /// <summary>
    /// Obtém um curso específico pelo seu Id.
    /// </summary>
    /// <param name="id">O Id (Guid) do curso a ser buscado.</param>
    /// <returns>O curso correspondente ao Id, ou nulo se não for encontrado.</returns>
    Task<CursoResponse?> GetById(Guid id);
    
    Task<CursoResponse> Update(Guid id, CursoRequest request);
    
    Task<bool> Delete(Guid id);
}