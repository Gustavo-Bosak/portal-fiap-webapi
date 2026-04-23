namespace PortalFIAP.Application.DTO;

public record AlunoResponse(
    Guid Id,
    string Nome,
    string Email,
    string Telefone,
    int Idade,
    string Logradouro,
    string Estado,
    string Cidade,
    string Bairro,
    string Cep,
    IEnumerable<MatriculaResponse> Matriculas
);
