using PortalFiap.Domain.Entities;
using PortalFiap.Domain.Enums;

namespace PortalFIAP.Application.DTO;

public record CursoRequest(
    NomeCurso Nome,
    int CargaHoraria
);