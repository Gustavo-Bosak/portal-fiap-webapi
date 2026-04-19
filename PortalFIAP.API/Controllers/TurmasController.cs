using Microsoft.AspNetCore.Mvc;
using PortalFIAP.Application.Interfaces;

namespace PortalFiap.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TurmasController : ControllerBase
{
    private readonly ITurmaService _turmaService;

    public TurmasController(ITurmaService turmaService)
    {
        _turmaService = turmaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var turmas = await _turmaService.GetAll();
        return Ok(turmas);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var turma = await _turmaService.GetById(id);
        if (turma is null)
            return NotFound();

        return Ok(turma);
    }
}
