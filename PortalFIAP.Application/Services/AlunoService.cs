using PortalFiap.Domain.Entities;
using PortalFIAP.Application.DTO;
using PortalFIAP.Application.Interfaces;
using PortalFIAP.Application.Interfaces.Repositories;

namespace PortalFIAP.Application.Services;

public class AlunoService : IAlunoService
{
    private readonly IAlunoRepository _repository;

    public AlunoService(IAlunoRepository repository)
    {
        _repository = repository;
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
        return ToResponse(aluno);
    }

    public async Task<AlunoResponse?> UpdateAsync(Guid id, AlunoRequest request)
    {
        var aluno = await _repository.GetByIdAsync(id);
        if (aluno is null) return null;

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
        return ToResponse(aluno);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
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
