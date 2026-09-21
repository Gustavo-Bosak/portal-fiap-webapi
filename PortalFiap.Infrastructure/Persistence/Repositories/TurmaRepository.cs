using Microsoft.EntityFrameworkCore;
using PortalFiap.Domain.Entities;
using PortalFIAP.Application.Interfaces.Repositories;

namespace PortalFiap.Infrastructure.Persistence.Repositories;

public class TurmaRepository : Repository<Turma>, ITurmaRepository
{
    public TurmaRepository(PortalFiapContext context) : base(context)
    {
    }

    protected override IQueryable<Turma> Query() => base.Query().Include(t => t.Curso);

    public async Task<IReadOnlyList<Turma>> GetByCursoIdAsync(Guid cursoId) =>
        await Query().AsNoTracking().Where(t => t.Curso.Id == cursoId).ToListAsync();
}
