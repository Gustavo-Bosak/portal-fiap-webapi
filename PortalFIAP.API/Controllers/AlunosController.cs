using Microsoft.AspNetCore.Mvc;
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
        var alunos = await _alunoService.GetAll();
        return Ok(alunos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var aluno = await _alunoService.GetById(id);
        if (aluno is null)
            return NotFound();

        return Ok(aluno);
    }
}
