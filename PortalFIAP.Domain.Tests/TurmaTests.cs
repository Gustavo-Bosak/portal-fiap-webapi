using PortalFiap.Domain.Entities;
using PortalFiap.Domain.Enums;
using PortalFiap.Domain.Exceptions;

namespace PortalFIAP.Domain.Tests;

public class TurmaTests
{
    private static Turma CriarTurma(string nome = "1TDSPZ", int? ano = null, int semestre = 4)
    {
        var curso = new Curso(NomeCurso.AnaliseEDesenvolvimentoDeSistemas, 2400);
        return new Turma(nome, ano ?? DateTime.Now.Year, semestre, curso,
            new List<Matricula>(), new List<Professor>());
    }

    [Fact]
    public void CriarTurma_DadosValidos_DefineCampos()
    {
        // Arrange
        var anoAtual = DateTime.Now.Year;

        // Act
        var turma = CriarTurma("2TDSPX", anoAtual, 6);

        // Assert
        Assert.Equal("2TDSPX", turma.NomeTurma);
        Assert.Equal(anoAtual, turma.AnoLetivo);
        Assert.Equal(6, turma.Semestre);
        Assert.NotNull(turma.Curso);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CriarTurma_NomeVazio_LancaDomainException(string? nome)
    {
        // Arrange / Act
        var ex = Assert.Throws<DomainException>(() => CriarTurma(nome: nome!));

        // Assert
        Assert.Equal("Nome da turma não pode estar vazio.", ex.Message);
    }

    [Theory]
    [InlineData(1989)]
    [InlineData(1900)]
    [InlineData(0)]
    public void CriarTurma_AnoAnteriorA1990_LancaDomainException(int ano)
    {
        // Arrange / Act
        var ex = Assert.Throws<DomainException>(() => CriarTurma(ano: ano));

        // Assert
        Assert.Contains("Ano letivo precisa estar entre 1990", ex.Message);
    }

    [Fact]
    public void CriarTurma_AnoAcimaDoLimiteDeCincoAnos_LancaDomainException()
    {
        // Arrange
        var anoInvalido = DateTime.Now.Year + 6;

        // Act
        var ex = Assert.Throws<DomainException>(() => CriarTurma(ano: anoInvalido));

        // Assert
        Assert.Contains($"{DateTime.Now.Year + 5}", ex.Message);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(9)]
    [InlineData(0)]
    [InlineData(-1)]
    public void CriarTurma_SemestreForaDaFaixa_LancaDomainException(int semestre)
    {
        // Arrange / Act
        var ex = Assert.Throws<DomainException>(() => CriarTurma(semestre: semestre));

        // Assert
        Assert.Equal("Semestre precisa estar entre 4 e 8.", ex.Message);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(8)]
    public void CriarTurma_SemestreNasBordas_Aceita(int semestre)
    {
        // Arrange / Act
        var turma = CriarTurma(semestre: semestre);

        // Assert
        Assert.Equal(semestre, turma.Semestre);
    }
}
