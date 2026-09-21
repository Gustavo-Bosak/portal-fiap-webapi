namespace PortalFIAP.Application.DTO;

/// <summary>
/// Dados para criar ou atualizar uma turma.
/// </summary>
/// <param name="NomeTurma">Nome da turma (ex.: 2TDSPK).</param>
/// <param name="AnoLetivo">Ano letivo (entre 1990 e o ano atual + 5).</param>
/// <param name="Semestre">Semestre (entre 4 e 8).</param>
/// <param name="CursoId">Id do curso ao qual a turma pertence.</param>
public record TurmaRequest(
    string NomeTurma,
    int AnoLetivo,
    int Semestre,
    Guid CursoId
);
