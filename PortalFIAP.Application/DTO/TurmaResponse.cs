using PortalFiap.Domain.Entities;
using PortalFiap.Domain.Enums;

namespace PortalFIAP.Application.DTO;

/// <summary>
/// Representação de uma turma retornada pela API (sem expor entidades de domínio).
/// </summary>
public record TurmaResponse(
    Guid Id,
    string NomeTurma,
    int AnoLetivo,
    int Semestre,
    Guid CursoId,
    NomeCurso Curso
)
{
    public static TurmaResponse FromDomain(Turma turma) =>
        new(turma.Id, turma.NomeTurma, turma.AnoLetivo, turma.Semestre, turma.Curso.Id, turma.Curso.Nome);
}
