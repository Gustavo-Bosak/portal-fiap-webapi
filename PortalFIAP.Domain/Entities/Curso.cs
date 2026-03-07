using PortalFIAP.Domain.Commom;
using PortalFIAP.Domain.Enums;
using System;
using System.Collections.Generic;

namespace PortalFIAP.Domain.Entities
{
    public class Curso : BaseEntity
    {
        public NomeCurso Nome { get; private set; }
        public int CargaHoraria { get; private set; }
        public List<Turma> Turmas { get; private set; }


        //Retorna a sigla correspondente ao curso
        public string Sigla
        {
            get
            {
                string siglaCorrespondente;
                switch (Nome)
                {
                    case NomeCurso.AnaliseEDesenvolvimentoDeSistemas:
                        siglaCorrespondente = "ADS";
                        break;
                    case NomeCurso.EngenhariaDaComputacao:
                        siglaCorrespondente = "EC";
                        break;
                    case NomeCurso.EngenhariaDeSoftware:
                        siglaCorrespondente = "ES";
                        break;
                    case NomeCurso.SistemasDeInformacao:
                        siglaCorrespondente = "SI";
                        break;
                    case NomeCurso.InteligenciaArtificial:
                        siglaCorrespondente = "IA";
                        break;
                    default:
                        siglaCorrespondente = string.Empty;
                        break;
                }
                return siglaCorrespondente;
            }
        }

        public Curso(NomeCurso nome, int cargaHoraria)
        {
            DefinirNome(nome);
            DefinirCargaHoraria(cargaHoraria);
            Turmas = new List<Turma>();
        }

        //Funçao p validar e definir o nome do curso
        public void DefinirNome(NomeCurso novoNome)
        {
            if (!Enum.IsDefined(typeof(NomeCurso), novoNome))
                throw new Exception("Nome de curso inválido.");

            Nome = novoNome;
        }

        //Funçao p validar e definir a carga horaria do curso 
        public void DefinirCargaHoraria(int novaCargaHoraria)
        {
            if (novaCargaHoraria <= 0)
                throw new Exception("A carga horária deve ser um número positivo.");

            CargaHoraria = novaCargaHoraria;
        }
    }
}
