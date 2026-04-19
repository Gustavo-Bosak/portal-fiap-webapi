using PortalFiap.Domain.Commom;

namespace PortalFiap.Domain.Entities;

public class Professor : Pessoa
{
    public List<Turma> Turmas { get; private set;}

    private Professor() : base() { }

    public Professor(string nome,
            string email,
            DateOnly dataNascimento,
            string telefone,
            Endereco endereco,
            List<Turma> turmas)
            //Superclasse Pessoa
            : base(nome, email, dataNascimento, telefone, endereco)
    {
        Turmas = turmas;
    }
}
