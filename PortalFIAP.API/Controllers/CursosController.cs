using Microsoft.AspNetCore.Mvc;
using PortalFIAP.Application.Interfaces;

namespace PortalFiap.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CursosController : ControllerBase
{
    private readonly ICursoService _cursoService;

    public CursosController(ICursoService cursoService)
    {
        _cursoService = cursoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cursos = await _cursoService.GetAll();
        return Ok(cursos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var curso = await _cursoService.GetById(id);
        if (curso is null)
            return NotFound();

        return Ok(curso);
    }
}
