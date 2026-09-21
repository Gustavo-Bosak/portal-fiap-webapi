using Microsoft.Extensions.Logging;
using PortalFIAP.Application.DTO;
using PortalFiap.Domain.Entities;
using PortalFiap.Domain.Exceptions;
using PortalFIAP.Application.Interfaces;
using PortalFIAP.Application.Interfaces.Repositories;

namespace PortalFIAP.Application.Services;

public class CursoService : ICursoService
{
    private readonly ICursoRepository _repository;
    private readonly ILogger<CursoService> _logger;

    public CursoService(ICursoRepository repository, ILogger<CursoService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<CursoResponse> Create(CursoRequest request)
    {
        _logger.LogInformation("Criando curso {Nome}", request.Nome);

        var curso = new Curso(request.Nome, request.CargaHoraria);

        await _repository.AddAsync(curso);
        _logger.LogInformation("Curso {CursoId} criado com sucesso", curso.Id);
        return CursoResponse.FromDomain(curso);
    }

    public async Task<IReadOnlyList<CursoResponse>> GetAll()
    {
        var cursos = await _repository.GetAllAsync();
        return cursos.Select(CursoResponse.FromDomain).ToList();
    }

    public async Task<CursoResponse?> GetById(Guid id)
    {
        var curso = await _repository.GetByIdAsync(id);
        return curso is null ? null : CursoResponse.FromDomain(curso);
    }

    public async Task<CursoResponse> Update(Guid id, CursoRequest request)
    {
        _logger.LogInformation("Atualizando curso {CursoId}", id);

        var curso = await _repository.GetByIdAsync(id);
        if (curso is null)
        {
            _logger.LogWarning("Curso {CursoId} não encontrado para atualização", id);
            throw new ResourceNotFoundException("Curso", id);
        }

        curso.DefinirNome(request.Nome);
        curso.DefinirCargaHoraria(request.CargaHoraria);

        await _repository.UpdateAsync(curso);
        _logger.LogInformation("Curso {CursoId} atualizado com sucesso", id);
        return CursoResponse.FromDomain(curso);
    }

    public async Task<bool> Delete(Guid id)
    {
        var removido = await _repository.DeleteAsync(id);
        if (removido)
            _logger.LogInformation("Curso {CursoId} removido com sucesso", id);
        else
            _logger.LogWarning("Curso {CursoId} não encontrado para remoção", id);
        return removido;
    }
}
