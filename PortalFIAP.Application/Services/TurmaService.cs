using PortalFIAP.Application.DTO;
using PortalFiap.Domain.Entities;
using PortalFiap.Domain.Exceptions;
using PortalFIAP.Application.Interfaces;
using PortalFIAP.Application.Interfaces.Repositories;

namespace PortalFIAP.Application.Services;

public class TurmaService : ITurmaService
{
    private readonly ITurmaRepository _turmaRepository;
    private readonly ICursoRepository _cursoRepository;

    public TurmaService(ITurmaRepository turmaRepository, ICursoRepository cursoRepository)
    {
        _turmaRepository = turmaRepository;
        _cursoRepository = cursoRepository;
    }

    public async Task<IReadOnlyList<TurmaResponse>> GetAll()
    {
        var turmas = await _turmaRepository.GetAllAsync();
        return turmas.Select(TurmaResponse.FromDomain).ToList();
    }

    public async Task<TurmaResponse?> GetById(Guid id)
    {
        var turma = await _turmaRepository.GetByIdAsync(id);
        return turma is null ? null : TurmaResponse.FromDomain(turma);
    }

    public async Task<IReadOnlyList<TurmaResponse>> GetByCursoId(Guid cursoId)
    {
        var turmas = await _turmaRepository.GetByCursoIdAsync(cursoId);
        return turmas.Select(TurmaResponse.FromDomain).ToList();
    }

    public async Task<TurmaResponse> Create(TurmaRequest request)
    {
        // Usa o curso rastreado pelo contexto para o EF não tentar inserir o Curso novamente.
        var curso = await _cursoRepository.GetByIdAsync(request.CursoId)
                    ?? throw new ResourceNotFoundException("Curso", request.CursoId);

        var turma = new Turma(
            request.NomeTurma,
            request.AnoLetivo,
            request.Semestre,
            curso,
            new List<Matricula>(),
            new List<Professor>()
        );

        await _turmaRepository.AddAsync(turma);
        return TurmaResponse.FromDomain(turma);
    }

    public async Task<TurmaResponse> Update(Guid id, TurmaRequest request)
    {
        var turma = await _turmaRepository.GetByIdAsync(id)
                    ?? throw new ResourceNotFoundException("Turma", id);

        var curso = await _cursoRepository.GetByIdAsync(request.CursoId)
                    ?? throw new ResourceNotFoundException("Curso", request.CursoId);

        turma.DefinirNomeTurma(request.NomeTurma);
        turma.DefinirAnoLetivo(request.AnoLetivo);
        turma.DefinirSemestre(request.Semestre);
        turma.Curso = curso;

        await _turmaRepository.UpdateAsync(turma);
        return TurmaResponse.FromDomain(turma);
    }

    public async Task<bool> Delete(Guid id) => await _turmaRepository.DeleteAsync(id);
}
