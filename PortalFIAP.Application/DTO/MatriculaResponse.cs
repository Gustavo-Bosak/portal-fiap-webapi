namespace PortalFIAP.Application.DTO;

public record MatriculaResponse(
    Guid Id,
    Guid TurmaId,
    Guid? BolsaId
);
