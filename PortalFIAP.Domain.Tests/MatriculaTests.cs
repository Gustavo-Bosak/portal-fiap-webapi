using PortalFiap.Domain.Entities;
using PortalFiap.Domain.Enums;

namespace PortalFIAP.Domain.Tests;

public class MatriculaTests
{
    private static Aluno CriarAluno() =>
        new("Maria Silva", "maria@fiap.com.br", DateOnly.FromDateTime(DateTime.Today).AddYears(-20),
            "11999999999", new Endereco("Rua A", "SP", "São Paulo", "Centro", "01000-000"),
            new List<Matricula>());

    private static Turma CriarTurma() =>
        new("1TDSPZ", DateTime.Now.Year, 4, new Curso(NomeCurso.EngenhariaDeSoftware, 3200),
            new List<Matricula>(), new List<Professor>());

    [Fact]
    public void CriarMatricula_AlunoETurmaValidos_SemBolsa_DefineVinculos()
    {
        // Arrange
        var aluno = CriarAluno();
        var turma = CriarTurma();

        // Act
        var matricula = new Matricula(aluno, turma, null);

        // Assert
        Assert.Same(aluno, matricula.Aluno);
        Assert.Same(turma, matricula.Turma);
        Assert.Null(matricula.Bolsa);
    }

    [Fact]
    public void CriarMatricula_ComBolsa_MantemBolsa()
    {
        // Arrange
        var bolsa = new Bolsa(Guid.NewGuid(), 0.5m, DateOnly.FromDateTime(DateTime.Today).AddMonths(6));

        // Act
        var matricula = new Matricula(CriarAluno(), CriarTurma(), bolsa);

        // Assert
        Assert.Same(bolsa, matricula.Bolsa);
    }

    [Fact]
    public void CriarMatricula_AlunoNulo_LancaArgumentNullException()
    {
        // Arrange
        var turma = CriarTurma();

        // Act
        var ex = Assert.Throws<ArgumentNullException>(() => new Matricula(null!, turma, null));

        // Assert
        Assert.Equal("aluno", ex.ParamName);
    }

    [Fact]
    public void CriarMatricula_TurmaNula_LancaArgumentNullException()
    {
        // Arrange
        var aluno = CriarAluno();

        // Act
        var ex = Assert.Throws<ArgumentNullException>(() => new Matricula(aluno, null!, null));

        // Assert
        Assert.Equal("turma", ex.ParamName);
    }
}
