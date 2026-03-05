using PortalFIAP.Domain.Commom;

namespace PortalFIAP.Domain.Entities;

public class Turma : BaseEntity
{
    public string NomeTurma { get; set; }
    public int AnoLetivo { get; set; }
    public int Semestre { get; set; }
    public string Curso { get; set; }
    public List<Matricula> Matriculas { get; set; }
    public List<Professor> Professores { get; set; }

    public Turma(string nomeTurma, int anoLetivo, int semestre, string curso, List<Matricula> matriculas, List<Professor> professores)
    {
        DefinirNomeTurma(nomeTurma);
        DefinirAnoLetivo(anoLetivo);
        DefinirSemestre(semestre);
        Curso = curso;
        Matriculas = matriculas;
        Professores = professores;
    }

    private void DefinirNomeTurma(string nomeTurma)
    {
        if (string.IsNullOrWhiteSpace(nomeTurma))
        {
            throw new Exception("Telefone nao pode estar vazio.");
        }
        NomeTurma = nomeTurma;
    }

    private void DefinirAnoLetivo(int anoLetivo)
    {
        if (anoLetivo <= new DateOnly().DayOfYear)
        {
            throw new Exception("Telefone nao pode estar vazio.");
        }
        AnoLetivo = anoLetivo;
    }

    private void DefinirSemestre(int semestre)
    {
        if (semestre is <= 0 or > 12)
        {
            throw new Exception("Telefone nao pode estar vazio.");
        }
        Semestre = semestre;
    }
}