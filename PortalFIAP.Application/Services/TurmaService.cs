using PortalFiap.Domain.Entities;
using PortalFIAP.Application.Interfaces;
using PortalFIAP.Application.Interfaces.Repositories;

namespace PortalFIAP.Application.Services;

public class TurmaService : ITurmaService
{
    private readonly ITurmaRepository _repository;

    public TurmaService(ITurmaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Turma>> GetAll() => await _repository.GetAllAsync();

    public async Task<Turma?> GetById(Guid id) => await _repository.GetByIdAsync(id);

    public async Task<IEnumerable<Turma>> GetByCursoId(Guid cursoId) =>
        await _repository.GetByCursoIdAsync(cursoId);
}
