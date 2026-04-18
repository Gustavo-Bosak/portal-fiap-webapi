using PortalFiap.Domain.Commom;

namespace PortalFiap.Domain.Entities;

public class Aluno : Pessoa
{
    public List<Matricula> Matriculas { get; private set;}

    public Aluno(string nome,
            string email,
            DateOnly dataNascimento,
            string telefone,
            Endereco endereco,
            List<Matricula> matriculas)
        //Superclasse Pessoa
        : base(nome, email, dataNascimento, telefone, endereco)
    {
        Matriculas = matriculas;
    }
}
