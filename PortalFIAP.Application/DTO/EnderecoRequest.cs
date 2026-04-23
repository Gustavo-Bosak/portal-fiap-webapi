namespace PortalFIAP.Application.DTO;

public record EnderecoRequest(
    string Logradouro,
    string Estado,
    string Cidade,
    string Bairro,
    string Cep
);
