using PortalFIAP.Application.DTO;
using PortalFiap.Domain.Entities;
using PortalFiap.Domain.Exceptions;
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
        var curso = new Curso(request.Nome, request.CargaHoraria);

        await _repository.AddAsync(curso);
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
        var curso = await _repository.GetByIdAsync(id)
                    ?? throw new ResourceNotFoundException("Curso", id);

        curso.DefinirNome(request.Nome);
        curso.DefinirCargaHoraria(request.CargaHoraria);

        await _repository.UpdateAsync(curso);
        return CursoResponse.FromDomain(curso);
    }

    public async Task<bool> Delete(Guid id) => await _repository.DeleteAsync(id);
}
