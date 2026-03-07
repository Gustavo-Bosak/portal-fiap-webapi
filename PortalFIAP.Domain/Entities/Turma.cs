using PortalFIAP.Domain.Commom;

namespace PortalFIAP.Domain.Entities;

public class Turma : BaseEntity
{
    public string NomeTurma { get; set; }
    public int AnoLetivo { get; set; }
    public int Semestre { get; set; }
    public Curso Curso { get; set; }
    public List<Matricula> Matriculas { get; set; }
    public List<Professor> Professores { get; set; }

    public Turma(string nomeTurma, int anoLetivo, int semestre, Curso curso, List<Matricula> matriculas, List<Professor> professores)
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
            throw new Exception("Nome da turma não pode estar vazio.");
        }
        NomeTurma = nomeTurma;
    }

    private void DefinirAnoLetivo(int anoLetivo)
    {
        var anoLimite = DateTime.Now.Year + 5;
        if (anoLetivo < 1990 || anoLetivo > anoLimite)
        {
            throw new Exception($"Ano letivo precisa estar entre 1990 e {anoLimite}.");
        }
        AnoLetivo = anoLetivo;
    }

    private void DefinirSemestre(int semestre)
    {
        if (semestre is < 4 or > 8 )
        {
            throw new Exception("Semestre precisa estar entre 4 e 8.");
        }
        Semestre = semestre;
    }
}