using Microsoft.Extensions.Logging;
using PortalFiap.Domain.Entities;
using PortalFiap.Domain.Exceptions;
using PortalFIAP.Application.DTO;
using PortalFIAP.Application.Interfaces;
using PortalFIAP.Application.Interfaces.Repositories;

namespace PortalFIAP.Application.Services;

public class AlunoService : IAlunoService
{
    private readonly IAlunoRepository _repository;
    private readonly ILogger<AlunoService> _logger;

    public AlunoService(IAlunoRepository repository, ILogger<AlunoService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<AlunoResponse>> GetAllAsync()
    {
        var alunos = await _repository.GetAllAsync();
        return alunos.Select(ToResponse);
    }

    public async Task<AlunoResponse?> GetByIdAsync(Guid id)
    {
        var aluno = await _repository.GetByIdAsync(id);
        return aluno is null ? null : ToResponse(aluno);
    }

    public async Task<AlunoResponse> CreateAsync(AlunoRequest request)
    {
        _logger.LogInformation("Criando aluno {Email}", request.Email);

        var endereco = new Endereco(
            request.Endereco.Logradouro,
            request.Endereco.Estado,
            request.Endereco.Cidade,
            request.Endereco.Bairro,
            request.Endereco.Cep
        );

        var aluno = new Aluno(
            request.Nome,
            request.Email,
            request.DataNascimento,
            request.Telefone,
            endereco,
            new List<Matricula>()
        );

        await _repository.AddAsync(aluno);
        _logger.LogInformation("Aluno {AlunoId} criado com sucesso", aluno.Id);
        return ToResponse(aluno);
    }

    public async Task<AlunoResponse> UpdateAsync(Guid id, AlunoRequest request)
    {
        _logger.LogInformation("Atualizando aluno {AlunoId}", id);

        var aluno = await _repository.GetByIdAsync(id);
        if (aluno is null)
        {
            _logger.LogWarning("Aluno {AlunoId} não encontrado para atualização", id);
            throw new ResourceNotFoundException("Aluno", id);
        }

        aluno.DefinirNome(request.Nome);
        aluno.DefinirEmail(request.Email);
        aluno.DefinirTelefone(request.Telefone);
        aluno.DefinirDataNasc(request.DataNascimento);
        aluno.Endereco.DefinirLogradouro(request.Endereco.Logradouro);
        aluno.Endereco.DefinirEstado(request.Endereco.Estado);
        aluno.Endereco.DefinirCidade(request.Endereco.Cidade);
        aluno.Endereco.DefinirBairro(request.Endereco.Bairro);
        aluno.Endereco.DefinirCep(request.Endereco.Cep);

        await _repository.UpdateAsync(aluno);
        _logger.LogInformation("Aluno {AlunoId} atualizado com sucesso", id);
        return ToResponse(aluno);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var removido = await _repository.DeleteAsync(id);
        if (removido)
            _logger.LogInformation("Aluno {AlunoId} removido com sucesso", id);
        else
            _logger.LogWarning("Aluno {AlunoId} não encontrado para remoção", id);
        return removido;
    }

    private static AlunoResponse ToResponse(Aluno aluno) => new(
        aluno.Id,
        aluno.Nome,
        aluno.Email,
        aluno.Telefone,
        aluno.Idade,
        aluno.Endereco.Logradouro,
        aluno.Endereco.Estado,
        aluno.Endereco.Cidade,
        aluno.Endereco.Bairro,
        aluno.Endereco.Cep,
        aluno.Matriculas.Select(m => new MatriculaResponse(m.Id, m.Turma.Id, m.Bolsa?.Id))
    );
}
