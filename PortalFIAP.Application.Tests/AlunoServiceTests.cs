using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using PortalFiap.Domain.Entities;
using PortalFiap.Domain.Exceptions;
using PortalFIAP.Application.DTO;
using PortalFIAP.Application.Interfaces.Repositories;
using PortalFIAP.Application.Services;

namespace PortalFIAP.Application.Tests;

public class AlunoServiceTests
{
    private readonly Mock<IAlunoRepository> _repository = new();
    private readonly AlunoService _service;

    public AlunoServiceTests()
    {
        _service = new AlunoService(_repository.Object, NullLogger<AlunoService>.Instance);
    }

    private static AlunoRequest RequestValido(string email = "ana@fiap.com.br") => new(
        "Ana Souza",
        email,
        "11999990000",
        new DateOnly(2000, 1, 1),
        new EnderecoRequest("Av. Paulista, 1000", "SP", "São Paulo", "Bela Vista", "01310100"));

    [Fact]
    public async Task CreateAsync_DadosValidos_PersisteUmaVezERetornaResponse()
    {
        // Arrange
        var request = RequestValido();

        // Act
        var response = await _service.CreateAsync(request);

        // Assert
        Assert.Equal("Ana Souza", response.Nome);
        Assert.Equal("ana@fiap.com.br", response.Email);
        _repository.Verify(r => r.AddAsync(It.IsAny<Aluno>()), Times.Once);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("email-sem-arroba")]
    public async Task CreateAsync_EmailInvalido_LancaDomainExceptionENaoPersiste(string email)
    {
        // Arrange
        var request = RequestValido(email);

        // Act
        var act = () => _service.CreateAsync(request);

        // Assert
        await Assert.ThrowsAsync<DomainException>(act);
        _repository.Verify(r => r.AddAsync(It.IsAny<Aluno>()), Times.Never);
    }

    [Fact]
    public async Task UpdateAsync_AlunoInexistente_RetornaNullENaoAtualiza()
    {
        // Arrange
        var id = Guid.NewGuid();
        _repository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Aluno?)null);

        // Act
        var response = await _service.UpdateAsync(id, RequestValido());

        // Assert
        Assert.Null(response);
        _repository.Verify(r => r.UpdateAsync(It.IsAny<Aluno>()), Times.Never);
    }

    [Fact]
    public async Task UpdateAsync_AlunoExistente_AtualizaUmaVez()
    {
        // Arrange
        var aluno = new Aluno(
            "Nome Antigo", "antigo@fiap.com.br", new DateOnly(2000, 1, 1), "11900000000",
            new Endereco("Rua A", "SP", "São Paulo", "Centro", "01000000"),
            new List<Matricula>());
        _repository.Setup(r => r.GetByIdAsync(aluno.Id)).ReturnsAsync(aluno);

        // Act
        var response = await _service.UpdateAsync(aluno.Id, RequestValido());

        // Assert
        Assert.NotNull(response);
        Assert.Equal("Ana Souza", response!.Nome);
        _repository.Verify(r => r.UpdateAsync(aluno), Times.Once);
    }
}
