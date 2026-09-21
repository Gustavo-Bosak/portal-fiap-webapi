using Microsoft.AspNetCore.Mvc;
using PortalFIAP.Application.DTO;
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
    
    [HttpPost]
    [ProducesResponseType(typeof(CursoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CursoRequest request)
    {
        var curso = await _cursoService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = curso.Id }, curso);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CursoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] CursoRequest request)
    {
        var curso = await _cursoService.Update(id, request);
        return Ok(curso);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (!await _cursoService.Delete(id))
            return NotFound();

        return NoContent();
    }
}
