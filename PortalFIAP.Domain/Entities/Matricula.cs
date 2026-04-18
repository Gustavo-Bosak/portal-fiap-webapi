using PortalFiap.Domain.Commom;

namespace PortalFiap.Domain.Entities;

public class Matricula : BaseEntity
{
    public Aluno Aluno { get; set; }
    public Turma Turma { get; set; }
    public Bolsa? Bolsa { get; set; } 

    public Matricula(Aluno aluno, Turma turma, Bolsa? bolsa)
    {
        if (aluno is null)
            throw new ArgumentNullException(nameof(aluno), "O aluno não pode ser nulo.");

        if (turma is null)
            throw new ArgumentNullException(nameof(turma), "A turma não pode ser nula.");

        Aluno = aluno;
        Turma = turma;
        Bolsa = bolsa;
    }
}
