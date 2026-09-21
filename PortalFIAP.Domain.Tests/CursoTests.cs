using PortalFiap.Domain.Entities;
using PortalFiap.Domain.Enums;
using PortalFiap.Domain.Exceptions;

namespace PortalFIAP.Domain.Tests;

public class CursoTests
{
    [Fact]
    public void CriarCurso_DadosValidos_DefineCamposEIniciaTurmasVazia()
    {
        // Arrange
        var nome = NomeCurso.EngenhariaDeSoftware;

        // Act
        var curso = new Curso(nome, 3200);

        // Assert
        Assert.Equal(nome, curso.Nome);
        Assert.Equal(3200, curso.CargaHoraria);
        Assert.NotNull(curso.Turmas);
        Assert.Empty(curso.Turmas);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-3200)]
    public void DefinirCargaHoraria_ZeroOuNegativa_LancaDomainException(int carga)
    {
        // Arrange
        var curso = new Curso(NomeCurso.SistemasDeInformacao, 2400);

        // Act
        var ex = Assert.Throws<DomainException>(() => curso.DefinirCargaHoraria(carga));

        // Assert
        Assert.Equal("A carga horária deve ser um número positivo.", ex.Message);
        Assert.Equal(2400, curso.CargaHoraria);
    }

    [Fact]
    public void DefinirNome_EnumNaoDefinido_LancaDomainException()
    {
        // Arrange
        var curso = new Curso(NomeCurso.InteligenciaArtificial, 2000);

        // Act
        var ex = Assert.Throws<DomainException>(() => curso.DefinirNome((NomeCurso)999));

        // Assert
        Assert.Equal("Nome de curso inválido.", ex.Message);
        Assert.Equal(NomeCurso.InteligenciaArtificial, curso.Nome);
    }

    [Theory]
    [InlineData(NomeCurso.AnaliseEDesenvolvimentoDeSistemas, "ADS")]
    [InlineData(NomeCurso.EngenhariaDaComputacao, "EC")]
    [InlineData(NomeCurso.EngenhariaDeSoftware, "ES")]
    [InlineData(NomeCurso.SistemasDeInformacao, "SI")]
    [InlineData(NomeCurso.InteligenciaArtificial, "IA")]
    public void Sigla_CadaNomeCurso_RetornaSiglaCorrespondente(NomeCurso nome, string siglaEsperada)
    {
        // Arrange
        var curso = new Curso(nome, 2000);

        // Act
        var sigla = curso.Sigla;

        // Assert
        Assert.Equal(siglaEsperada, sigla);
    }
}
