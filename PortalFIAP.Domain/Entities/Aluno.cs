using PortalFIAP.Domain.Commom;

namespace PortalFIAP.Domain.Entities;

public class Aluno : Pessoa
{
    public List<string> Matriculas { get; private set;}

    public Aluno(string nome,
            string email,
            DateOnly dataNascimento,
            string telefone,
            string endereco,
            List<string> matriculas)
        //Superclasse Pessoa
        : base(nome, email, dataNascimento, telefone, endereco)
    {
        Matriculas = matriculas;
    }
}
