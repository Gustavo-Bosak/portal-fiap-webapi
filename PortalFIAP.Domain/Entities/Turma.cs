using PortalFiap.Domain.Exceptions;
using PortalFiap.Domain.Exceptions;
using PortalFiap.Domain.Commom;

namespace PortalFiap.Domain.Entities;

public class Turma : BaseEntity
{
    public string NomeTurma { get; set; }
    public int AnoLetivo { get; set; }
    public int Semestre { get; set; }
    public Curso Curso { get; set; }
    public List<Matricula> Matriculas { get; set; }
    public List<Professor> Professores { get; set; }

    private Turma() { }

    public Turma(string nomeTurma, int anoLetivo, int semestre, Curso curso, List<Matricula> matriculas, List<Professor> professores)
    {
        DefinirNomeTurma(nomeTurma);
        DefinirAnoLetivo(anoLetivo);
        DefinirSemestre(semestre);
        Curso = curso;
        Matriculas = matriculas;
        Professores = professores;
    }

    public void DefinirNomeTurma(string nomeTurma)
    {
        if (string.IsNullOrWhiteSpace(nomeTurma))
        {
            throw new DomainException("Nome da turma não pode estar vazio.");
        }
        NomeTurma = nomeTurma;
    }

    public void DefinirAnoLetivo(int anoLetivo)
    {
        var anoLimite = DateTime.Now.Year + 5;
        if (anoLetivo < 1990 || anoLetivo > anoLimite)
        {
            throw new DomainException($"Ano letivo precisa estar entre 1990 e {anoLimite}.");
        }
        AnoLetivo = anoLetivo;
    }

    public void DefinirSemestre(int semestre)
    {
        if (semestre is < 4 or > 8 )
        {
            throw new DomainException("Semestre precisa estar entre 4 e 8.");
        }
        Semestre = semestre;
    }
}