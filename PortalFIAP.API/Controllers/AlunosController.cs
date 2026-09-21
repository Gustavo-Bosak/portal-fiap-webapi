using Microsoft.AspNetCore.Mvc;
using PortalFIAP.Application.DTO;
using PortalFIAP.Application.Interfaces;

namespace PortalFiap.Controllers;

/// <summary>
/// Gerencia os alunos do portal.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AlunosController : ControllerBase
{
    private readonly IAlunoService _alunoService;

    public AlunosController(IAlunoService alunoService)
    {
        _alunoService = alunoService;
    }

    /// <summary>
    /// Lista todos os alunos ativos.
    /// </summary>
    /// <returns>Coleção de alunos com endereço e matrículas.</returns>
    /// <response code="200">Lista de alunos retornada com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AlunoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var alunos = await _alunoService.GetAllAsync();
        return Ok(alunos);
    }

    /// <summary>
    /// Obtém um aluno pelo Id.
    /// </summary>
    /// <param name="id">Id (GUID) do aluno.</param>
    /// <response code="200">Aluno encontrado.</response>
    /// <response code="404">Aluno não encontrado.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AlunoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var aluno = await _alunoService.GetByIdAsync(id);
        if (aluno is null)
            return NotFound();

        return Ok(aluno);
    }

    /// <summary>
    /// Cadastra um novo aluno.
    /// </summary>
    /// <remarks>
    /// A data de nascimento usa o formato <c>yyyy-MM-dd</c>. Nome, e-mail, telefone e endereço são validados pelo domínio.
    /// </remarks>
    /// <param name="request">Dados do aluno e do endereço.</param>
    /// <response code="201">Aluno criado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(AlunoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] AlunoRequest request)
    {
        var aluno = await _alunoService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = aluno.Id }, aluno);
    }

    /// <summary>
    /// Atualiza os dados de um aluno existente.
    /// </summary>
    /// <param name="id">Id (GUID) do aluno.</param>
    /// <param name="request">Novos dados do aluno e do endereço.</param>
    /// <response code="200">Aluno atualizado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    /// <response code="404">Aluno não encontrado.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(AlunoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(Guid id, [FromBody] AlunoRequest request)
    {
        var aluno = await _alunoService.UpdateAsync(id, request);
        if (aluno is null)
            return NotFound();

        return Ok(aluno);
    }

    /// <summary>
    /// Remove (exclusão lógica) um aluno.
    /// </summary>
    /// <param name="id">Id (GUID) do aluno.</param>
    /// <response code="204">Aluno removido com sucesso.</response>
    /// <response code="404">Aluno não encontrado.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _alunoService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
