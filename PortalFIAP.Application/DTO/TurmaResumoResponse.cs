using PortalFiap.Domain.Entities;

namespace PortalFIAP.Application.DTO;

/// <summary>
/// Resumo de turma usado dentro de outros recursos (ex.: Curso), evitando referência circular.
/// </summary>
public record TurmaResumoResponse(
    Guid Id,
    string NomeTurma,
    int AnoLetivo,
    int Semestre
)
{
    public static TurmaResumoResponse FromDomain(Turma turma) =>
        new(turma.Id, turma.NomeTurma, turma.AnoLetivo, turma.Semestre);
}
