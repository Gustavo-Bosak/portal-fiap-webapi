using Moq;
using PortalFiap.Domain.Entities;
using PortalFiap.Domain.Enums;
using PortalFiap.Domain.Exceptions;
using PortalFIAP.Application.DTO;
using PortalFIAP.Application.Interfaces.Repositories;
using PortalFIAP.Application.Services;

namespace PortalFIAP.Application.Tests;

public class TurmaServiceTests
{
    private readonly Mock<ITurmaRepository> _turmas = new();
    private readonly Mock<ICursoRepository> _cursos = new();
    private readonly TurmaService _service;

    public TurmaServiceTests()
    {
        _service = new TurmaService(_turmas.Object, _cursos.Object);
    }

    [Fact]
    public async Task Create_CursoInexistente_LancaResourceNotFoundENaoPersiste()
    {
        // Arrange
        var cursoId = Guid.NewGuid();
        _cursos.Setup(r => r.GetByIdAsync(cursoId)).ReturnsAsync((Curso?)null);
        var request = new TurmaRequest("ADS-2026-1", 2026, 4, cursoId);

        // Act
        var act = () => _service.Create(request);

        // Assert
        await Assert.ThrowsAsync<ResourceNotFoundException>(act);
        _turmas.Verify(r => r.AddAsync(It.IsAny<Turma>()), Times.Never);
    }

    [Fact]
    public async Task Create_CursoExistente_PersisteUmaVez()
    {
        // Arrange
        var curso = new Curso(NomeCurso.AnaliseEDesenvolvimentoDeSistemas, 2400);
        _cursos.Setup(r => r.GetByIdAsync(curso.Id)).ReturnsAsync(curso);
        var request = new TurmaRequest("ADS-2026-1", 2026, 4, curso.Id);

        // Act
        var response = await _service.Create(request);

        // Assert
        Assert.Equal("ADS-2026-1", response.NomeTurma);
        Assert.Equal(curso.Id, response.CursoId);
        _turmas.Verify(r => r.AddAsync(It.IsAny<Turma>()), Times.Once);
    }

    [Fact]
    public async Task Update_TurmaInexistente_LancaResourceNotFoundENaoAtualiza()
    {
        // Arrange
        var id = Guid.NewGuid();
        _turmas.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Turma?)null);

        // Act
        var act = () => _service.Update(id, new TurmaRequest("ADS-2026-1", 2026, 4, Guid.NewGuid()));

        // Assert
        await Assert.ThrowsAsync<ResourceNotFoundException>(act);
        _turmas.Verify(r => r.UpdateAsync(It.IsAny<Turma>()), Times.Never);
    }
}
