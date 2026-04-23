using Microsoft.EntityFrameworkCore;
using PortalFIAP.Application.DTO;
using PortalFiap.Domain.Entities;
using PortalFIAP.Application.Interfaces;
using PortalFIAP.Application.Interfaces.Repositories;

namespace PortalFIAP.Application.Services;

public class CursoService : ICursoService
{
    private readonly ICursoRepository _repository;

    public CursoService(ICursoRepository repository)
    {
        _repository = repository;
    }

    public async Task<CursoResponse> Create(CursoRequest request)
    {
        var curso = new Curso(
            request.Nome,
            request.CargaHoraria
        );

        await _repository.Add(curso);
        return CursoResponse.FromDomain(curso);
    }

    public async Task<IReadOnlyList<CursoResponse>> GetAll()
    {
        var alunos = await _repository.GetAll();
        return alunos.Select(c => CursoResponse.FromDomain(c)).ToList();
    }

    public async Task<CursoResponse?> GetById(Guid id)
    {
        var curso = await _repository.GetById(id);
        return curso is null  ? null : CursoResponse.FromDomain(curso);
    }
    
    public async Task<CursoResponse> Update(Guid id, CursoRequest request)
    {
        var curso = await _repository.GetById(id);
        if (curso is null) return null;
        
        curso.DefinirNome(request.Nome);
        curso.DefinirCargaHoraria(request.CargaHoraria);
        
        await _repository.Update(curso);
        return CursoResponse.FromDomain(curso);
    }

    public async Task<bool> Delete(Guid id) => await _repository.Delete(id);
}