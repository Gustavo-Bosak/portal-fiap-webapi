namespace PortalFIAP.Application.DTO;

public record AlunoRequest(
    string Nome,
    string Email,
    string Telefone,
    DateOnly DataNascimento,
    EnderecoRequest Endereco
);
