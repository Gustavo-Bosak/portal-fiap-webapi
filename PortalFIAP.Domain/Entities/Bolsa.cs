using PortalFIAP.Domain.Commom;
using System;

namespace PortalFIAP.Domain.Entities
{
    public class Bolsa : BaseEntity
    {
        public int IdMatricula { get; private set; }
        public decimal Desconto { get; private set; }
        public DateOnly Validade { get; private set; }
        
        public Bolsa(int idMatricula, decimal desconto, DateOnly validade)
        {
            if (idMatricula <= 0)
                throw new Exception("O ID da matrícula deve ser um número positivo.");
            
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
