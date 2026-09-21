using Microsoft.AspNetCore.Mvc;
using PortalFIAP.Application.DTO;
using PortalFIAP.Application.Interfaces;

namespace PortalFiap.Controllers;

/// <summary>
/// Gerencia as turmas, sempre vinculadas a um curso.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TurmasController : ControllerBase
{
    private readonly ITurmaService _turmaService;

    public TurmasController(ITurmaService turmaService)
    {
        _turmaService = turmaService;
    }

    /// <summary>
    /// Lista todas as turmas ativas.
    /// </summary>
    /// <response code="200">Lista de turmas retornada com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TurmaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var turmas = await _turmaService.GetAll();
        return Ok(turmas);
    }

    /// <summary>
    /// Obtém uma turma pelo Id.
    /// </summary>
    /// <param name="id">Id (GUID) da turma.</param>
    /// <response code="200">Turma encontrada.</response>
    /// <response code="404">Turma não encontrada.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TurmaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var turma = await _turmaService.GetById(id);
        if (turma is null)
            return NotFound();

        return Ok(turma);
    }

    /// <summary>
    /// Cria uma turma vinculada a um curso existente.
    /// </summary>
    /// <remarks>
    /// O semestre deve estar entre 4 e 8 e o ano letivo entre 1990 e o ano atual + 5.
    /// </remarks>
    /// <param name="request">Dados da turma, incluindo o Id do curso.</param>
    /// <response code="201">Turma criada com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    /// <response code="404">Curso informado não encontrado.</response>
    [HttpPost]
    [ProducesResponseType(typeof(TurmaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] TurmaRequest request)
    {
        var turma = await _turmaService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = turma.Id }, turma);
    }

    /// <summary>
    /// Atualiza uma turma existente.
    /// </summary>
    /// <param name="id">Id (GUID) da turma.</param>
    /// <param name="request">Novos dados da turma.</param>
    /// <response code="200">Turma atualizada com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    /// <response code="404">Turma ou curso não encontrado.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(TurmaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(Guid id, [FromBody] TurmaRequest request)
    {
        var turma = await _turmaService.Update(id, request);
        return Ok(turma);
    }

    /// <summary>
    /// Remove (exclusão lógica) uma turma.
    /// </summary>
    /// <param name="id">Id (GUID) da turma.</param>
    /// <response code="204">Turma removida com sucesso.</response>
    /// <response code="404">Turma não encontrada.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (!await _turmaService.Delete(id))
            return NotFound();

        return NoContent();
    }
}
