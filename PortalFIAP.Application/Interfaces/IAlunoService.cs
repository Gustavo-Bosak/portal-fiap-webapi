using PortalFiap.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalFIAP.Application.Interfaces;

/// <summary>
/// Contrato do serviço de Alunos. Define as operações que podem ser realizadas com os alunos.
/// </summary>
public interface IAlunoService
{
    /// <summary>
    /// Obtém todos os alunos cadastrados.
    /// </summary>
    /// <returns>Uma coleção de todos os alunos.</returns>
    Task<IEnumerable<Aluno>> GetAll();
    
    /// <summary>
    /// Obtém um aluno específico pelo seu Id.
    /// </summary>
    /// <param name="id">O Id (Guid) do aluno a ser buscado.</param>
    /// <returns>O aluno correspondente ao Id, ou nulo se não for encontrado.</returns>
    Task<Aluno?> GetById(Guid id);
}