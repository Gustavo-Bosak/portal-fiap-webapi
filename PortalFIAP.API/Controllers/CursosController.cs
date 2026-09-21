using Microsoft.AspNetCore.Mvc;
using PortalFIAP.Application.DTO;
using PortalFIAP.Application.Interfaces;

namespace PortalFiap.Controllers;

/// <summary>
/// Gerencia os cursos oferecidos pela instituição.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CursosController : ControllerBase
{
    private readonly ICursoService _cursoService;

    public CursosController(ICursoService cursoService)
    {
        _cursoService = cursoService;
    }

    /// <summary>
    /// Lista todos os cursos ativos com o resumo de suas turmas.
    /// </summary>
    /// <response code="200">Lista de cursos retornada com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CursoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var cursos = await _cursoService.GetAll();
        return Ok(cursos);
    }

    /// <summary>
    /// Obtém um curso pelo Id.
    /// </summary>
    /// <param name="id">Id (GUID) do curso.</param>
    /// <response code="200">Curso encontrado.</response>
    /// <response code="404">Curso não encontrado.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CursoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var curso = await _cursoService.GetById(id);
        if (curso is null)
            return NotFound();

        return Ok(curso);
    }

    /// <summary>
    /// Cadastra um novo curso.
    /// </summary>
    /// <param name="request">Nome (enum) e carga horária do curso.</param>
    /// <response code="201">Curso criado com sucesso.</response>
    /// <response code="400">Dados inválidos (nome inexistente ou carga horária não positiva).</response>
    [HttpPost]
    [ProducesResponseType(typeof(CursoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CursoRequest request)
    {
        var curso = await _cursoService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = curso.Id }, curso);
    }

    /// <summary>
    /// Atualiza um curso existente.
    /// </summary>
    /// <param name="id">Id (GUID) do curso.</param>
    /// <param name="request">Novos dados do curso.</param>
    /// <response code="200">Curso atualizado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    /// <response code="404">Curso não encontrado.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CursoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(Guid id, [FromBody] CursoRequest request)
    {
        var curso = await _cursoService.Update(id, request);
        return Ok(curso);
    }

    /// <summary>
    /// Remove (exclusão lógica) um curso.
    /// </summary>
    /// <param name="id">Id (GUID) do curso.</param>
    /// <response code="204">Curso removido com sucesso.</response>
    /// <response code="404">Curso não encontrado.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (!await _cursoService.Delete(id))
            return NotFound();

        return NoContent();
    }
}
