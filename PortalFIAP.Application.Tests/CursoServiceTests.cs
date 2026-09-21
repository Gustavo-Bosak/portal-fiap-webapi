using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using PortalFiap.Domain.Entities;
using PortalFiap.Domain.Enums;
using PortalFiap.Domain.Exceptions;
using PortalFIAP.Application.DTO;
using PortalFIAP.Application.Interfaces.Repositories;
using PortalFIAP.Application.Services;

namespace PortalFIAP.Application.Tests;

public class CursoServiceTests
{
    private readonly Mock<ICursoRepository> _repository = new();
    private readonly CursoService _service;

    public CursoServiceTests()
    {
        _service = new CursoService(_repository.Object, NullLogger<CursoService>.Instance);
    }

    [Fact]
    public async Task Create_DadosValidos_PersisteUmaVezERetornaResponse()
    {
        // Arrange
        var request = new CursoRequest(NomeCurso.EngenhariaDeSoftware, 2400);

        // Act
        var response = await _service.Create(request);

        // Assert
        Assert.Equal(NomeCurso.EngenhariaDeSoftware, response.Nome);
        Assert.Equal(2400, response.CargaHoraria);
        _repository.Verify(r => r.AddAsync(It.IsAny<Curso>()), Times.Once);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public async Task Create_CargaHorariaInvalida_LancaDomainExceptionENaoPersiste(int cargaHoraria)
    {
        // Arrange
        var request = new CursoRequest(NomeCurso.EngenhariaDeSoftware, cargaHoraria);

        // Act
        var act = () => _service.Create(request);

        // Assert
        await Assert.ThrowsAsync<DomainException>(act);
        _repository.Verify(r => r.AddAsync(It.IsAny<Curso>()), Times.Never);
    }

    [Fact]
    public async Task Update_CursoInexistente_LancaResourceNotFoundENaoAtualiza()
    {
        // Arrange
        var id = Guid.NewGuid();
        _repository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Curso?)null);

        // Act
        var act = () => _service.Update(id, new CursoRequest(NomeCurso.EngenhariaDeSoftware, 2400));

        // Assert
        await Assert.ThrowsAsync<ResourceNotFoundException>(act);
        _repository.Verify(r => r.UpdateAsync(It.IsAny<Curso>()), Times.Never);
    }

    [Fact]
    public async Task Update_CursoExistente_AtualizaUmaVez()
    {
        // Arrange
        var curso = new Curso(NomeCurso.SistemasDeInformacao, 2000);
        _repository.Setup(r => r.GetByIdAsync(curso.Id)).ReturnsAsync(curso);

        // Act
        var response = await _service.Update(curso.Id, new CursoRequest(NomeCurso.InteligenciaArtificial, 3000));

        // Assert
        Assert.Equal(NomeCurso.InteligenciaArtificial, response.Nome);
        Assert.Equal(3000, response.CargaHoraria);
        _repository.Verify(r => r.UpdateAsync(curso), Times.Once);
    }

    [Fact]
    public async Task Delete_CursoInexistente_RetornaFalse()
    {
        // Arrange
        var id = Guid.NewGuid();
        _repository.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

        // Act
        var removido = await _service.Delete(id);

        // Assert
        Assert.False(removido);
    }
}
