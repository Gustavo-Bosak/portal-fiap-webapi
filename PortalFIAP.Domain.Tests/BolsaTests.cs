using System.Globalization;
using PortalFiap.Domain.Entities;
using PortalFiap.Domain.Exceptions;

namespace PortalFIAP.Domain.Tests;

public class BolsaTests
{
    private static DateOnly Hoje => DateOnly.FromDateTime(DateTime.Today);

    [Fact]
    public void CriarBolsa_DadosValidos_DefineCampos()
    {
        // Arrange
        var idMatricula = Guid.NewGuid();
        var validade = Hoje.AddMonths(6);

        // Act
        var bolsa = new Bolsa(idMatricula, 0.5m, validade);

        // Assert
        Assert.Equal(idMatricula, bolsa.IdMatricula);
        Assert.Equal(0.5m, bolsa.Desconto);
        Assert.Equal(validade, bolsa.Validade);
    }

    [Fact]
    public void AtualizarDesconto_DescontoIgualAUm_Aceita()
    {
        // Arrange
        var bolsa = new Bolsa(Guid.NewGuid(), 0.3m, Hoje.AddDays(30));

        // Act
        bolsa.AtualizarDesconto(1m);

        // Assert
        Assert.Equal(1m, bolsa.Desconto);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-0.1")]
    [InlineData("1.01")]
    public void AtualizarDesconto_ForaDoIntervalo_LancaDomainException(string valor)
    {
        // Arrange (decimal nao e constante de atributo, por isso parse invariante)
        var desconto = decimal.Parse(valor, CultureInfo.InvariantCulture);
        var bolsa = new Bolsa(Guid.NewGuid(), 0.3m, Hoje.AddDays(30));

        // Act
        var ex = Assert.Throws<DomainException>(() => bolsa.AtualizarDesconto(desconto));

        // Assert
        Assert.Equal("O desconto deve ser um valor maior que 0 e menor ou igual a 1.", ex.Message);
        Assert.Equal(0.3m, bolsa.Desconto);
    }

    [Fact]
    public void AtualizarValidade_ValidadeHoje_Aceita()
    {
        // Arrange
        var bolsa = new Bolsa(Guid.NewGuid(), 0.3m, Hoje.AddDays(30));

        // Act
        bolsa.AtualizarValidade(Hoje);

        // Assert
        Assert.Equal(Hoje, bolsa.Validade);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-30)]
    [InlineData(-365)]
    public void AtualizarValidade_DataNoPassado_LancaDomainException(int diasNoPassado)
    {
        // Arrange
        var bolsa = new Bolsa(Guid.NewGuid(), 0.3m, Hoje.AddDays(30));

        // Act
        var ex = Assert.Throws<DomainException>(() => bolsa.AtualizarValidade(Hoje.AddDays(diasNoPassado)));

        // Assert
        Assert.Equal("A data de validade não pode ser no passado.", ex.Message);
    }

    [Fact]
    public void CriarBolsa_IdMatriculaVazio_LancaDomainException()
    {
        // Arrange
        var idVazio = Guid.Empty;

        // Act
        var ex = Assert.Throws<DomainException>(() => new Bolsa(idVazio, 0.5m, Hoje.AddDays(10)));

        // Assert
        Assert.Equal("O ID da matrícula não pode ser vazio.", ex.Message);
    }
}
