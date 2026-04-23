using PortalFiap.Domain.Entities;
using PortalFiap.Domain.Enums;

namespace PortalFIAP.Application.DTO;

public record CursoResponse(
    Guid Id,
    NomeCurso Nome,
    int CargaHoraria,
    List<Turma> Turmas
)
{
    public static CursoResponse FromDomain(Curso curso) => new CursoResponse(curso.Id, curso.Nome, curso.CargaHoraria, curso.Turmas);
}