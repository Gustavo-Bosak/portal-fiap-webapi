using PortalFIAP.Domain.Commom;

namespace PortalFIAP.Domain.Entities;

public class Matricula : BaseEntity
{
    public Aluno Aluno { get; set; }
    public Turma Turma { get; set; }
    public string Bolsa { get; set; }

    public Matricula(Aluno aluno, Turma turma, string bolsa)
    {
        Aluno = aluno;
        Turma = turma;
        Bolsa = bolsa;
    }
}
