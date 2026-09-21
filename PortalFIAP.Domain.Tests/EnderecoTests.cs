using PortalFiap.Domain.Entities;
using PortalFiap.Domain.Exceptions;

namespace PortalFIAP.Domain.Tests;

public class EnderecoTests
{
    private static Endereco CriarEndereco() =>
        new("Rua A", "SP", "São Paulo", "Centro", "01000-000");

    [Fact]
    public void CriarEndereco_DadosValidos_DefineCampos()
    {
        // Arrange / Act
        var endereco = new Endereco("Av. Paulista, 1106", "SP", "São Paulo", "Bela Vista", "01311-000");

        // Assert
        Assert.Equal("Av. Paulista, 1106", endereco.Logradouro);
        Assert.Equal("SP", endereco.Estado);
        Assert.Equal("São Paulo", endereco.Cidade);
        Assert.Equal("Bela Vista", endereco.Bairro);
        Assert.Equal("01311-000", endereco.Cep);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void DefinirLogradouro_Vazio_LancaDomainException(string? valor)
    {
        // Arrange
        var endereco = CriarEndereco();

        // Act
        var ex = Assert.Throws<DomainException>(() => endereco.DefinirLogradouro(valor!));

        // Assert
        Assert.Equal("Logradouro não pode ser vazio.", ex.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void DefinirEstado_Vazio_LancaDomainException(string? valor)
    {
        // Arrange
        var endereco = CriarEndereco();

        // Act
        var ex = Assert.Throws<DomainException>(() => endereco.DefinirEstado(valor!));

        // Assert
        Assert.Equal("Estado não pode ser vazio.", ex.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void DefinirCidade_Vazia_LancaDomainException(string? valor)
    {
        // Arrange
        var endereco = CriarEndereco();

        // Act
        var ex = Assert.Throws<DomainException>(() => endereco.DefinirCidade(valor!));

        // Assert
        Assert.Equal("Cidade não pode ser vazia.", ex.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void DefinirBairro_Vazio_LancaDomainException(string? valor)
    {
        // Arrange
        var endereco = CriarEndereco();

        // Act
        var ex = Assert.Throws<DomainException>(() => endereco.DefinirBairro(valor!));

        // Assert
        Assert.Equal("Bairro não pode ser vazio.", ex.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void DefinirCep_Vazio_LancaDomainException(string? valor)
    {
        // Arrange
        var endereco = CriarEndereco();

        // Act
        var ex = Assert.Throws<DomainException>(() => endereco.DefinirCep(valor!));

        // Assert
        Assert.Equal("CEP não pode ser vazio.", ex.Message);
    }
}
