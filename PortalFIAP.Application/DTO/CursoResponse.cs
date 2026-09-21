using PortalFiap.Domain.Entities;
using PortalFiap.Domain.Enums;

namespace PortalFIAP.Application.DTO;

public record CursoResponse(
    Guid Id,
    NomeCurso Nome,
    int CargaHoraria,
    List<TurmaResumoResponse> Turmas
)
{
    public static CursoResponse FromDomain(Curso curso) => new CursoResponse(
        curso.Id,
        curso.Nome,
        curso.CargaHoraria,
        (curso.Turmas ?? new List<Turma>()).Where(t => t.Active).Select(TurmaResumoResponse.FromDomain).ToList());
}
