using PortalFIAP.Domain.Commom;

namespace PortalFIAP.Domain.Entities;

public class Professor : Pessoa
{
    public List<string> Turmas { get; private set;}

    public Professor(string nome,
            string email,
            DateOnly dataNascimento,
            string telefone,
            string endereco,
            List<string> turmas)
            //Superclasse Pessoa
            : base(nome, email, dataNascimento, telefone, endereco)
    {
        Turmas = turmas;
    }
}
