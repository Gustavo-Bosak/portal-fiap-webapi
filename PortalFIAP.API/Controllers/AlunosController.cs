using Microsoft.AspNetCore.Mvc;
using PortalFIAP.Application.DTO;
using PortalFIAP.Application.Interfaces;

namespace PortalFiap.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlunosController : ControllerBase
{
    private readonly IAlunoService _alunoService;

    public AlunosController(IAlunoService alunoService)
    {
        _alunoService = alunoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var alunos = await _alunoService.GetAllAsync();
        return Ok(alunos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var aluno = await _alunoService.GetByIdAsync(id);
        if (aluno is null)
            return NotFound();

        return Ok(aluno);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AlunoRequest request)
    {
        var aluno = await _alunoService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = aluno.Id }, aluno);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] AlunoRequest request)
    {
        var aluno = await _alunoService.UpdateAsync(id, request);
        if (aluno is null)
            return NotFound();

        return Ok(aluno);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _alunoService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}