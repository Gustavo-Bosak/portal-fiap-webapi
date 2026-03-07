using PortalFIAP.Domain.Commom;
using System;

namespace PortalFIAP.Domain.Entities
{
    public class Endereco : BaseEntity
    {
        public string Logradouro { get; private set; }
        public string Estado { get; private set; }
        public string Cidade { get; private set; }
        public string Bairro { get; private set; }
        public string Cep { get; private set; }

        public Endereco(string logradouro, string estado, string cidade, string bairro, string cep)
        {
            DefinirLogradouro(logradouro);
            DefinirEstado(estado);
            DefinirCidade(cidade);
            DefinirBairro(bairro);
            DefinirCep(cep);
        }


        //Funçoes p validar e definir os atributos
        public void DefinirLogradouro(string novoLogradouro)
        {
            if (string.IsNullOrWhiteSpace(novoLogradouro))
                throw new Exception("Logradouro não pode ser vazio.");
            Logradouro = novoLogradouro;
        }

        public void DefinirEstado(string novoEstado)
        {
            if (string.IsNullOrWhiteSpace(novoEstado))
                throw new Exception("Estado não pode ser vazio.");
            Estado = novoEstado;
        }

        public void DefinirCidade(string novaCidade)
        {
            if (string.IsNullOrWhiteSpace(novaCidade))
                throw new Exception("Cidade não pode ser vazia.");
            Cidade = novaCidade;
        }

        public void DefinirBairro(string novoBairro)
        {
            if (string.IsNullOrWhiteSpace(novoBairro))
                throw new Exception("Bairro não pode ser vazio.");
            Bairro = novoBairro;
        }

        public void DefinirCep(string novoCep)
        {
            if (string.IsNullOrWhiteSpace(novoCep))
                throw new Exception("CEP não pode ser vazio.");
            Cep = novoCep;
        }
    }
}
