using PortalFiap.Domain.Commom;
using System;

namespace PortalFiap.Domain.Entities
{
    public class Bolsa : BaseEntity
    {
        public Guid IdMatricula { get; private set; }
        public decimal Desconto { get; private set; }
        public DateOnly Validade { get; private set; }
        
        private Bolsa() { }

        public Bolsa(Guid idMatricula, decimal desconto, DateOnly validade)
        {
            if (idMatricula == Guid.Empty)
                throw new Exception("O ID da matrícula não pode ser vazio.");
            
            IdMatricula = idMatricula;
            AtualizarDesconto(desconto);
            AtualizarValidade(validade);
        }

        //Funçao validar desconto da bolsa
        public void AtualizarDesconto(decimal novoDesconto)
        {
            if (novoDesconto <= 0 || novoDesconto > 1)
                throw new Exception("O desconto deve ser um valor maior que 0 e menor ou igual a 1.");
            
            Desconto = novoDesconto;
        }

        //Funçao validar validade da bolsa
        public void AtualizarValidade(DateOnly novaValidade)
        {
            if (novaValidade < DateOnly.FromDateTime(DateTime.Today))
                throw new Exception("A data de validade não pode ser no passado.");

            Validade = novaValidade;
        }
    }
}
