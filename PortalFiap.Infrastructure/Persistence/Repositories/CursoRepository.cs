using Microsoft.EntityFrameworkCore;
using PortalFiap.Domain.Entities;
using PortalFIAP.Application.Interfaces.Repositories;

namespace PortalFiap.Infrastructure.Persistence.Repositories;

public class CursoRepository : Repository<Curso>, ICursoRepository
{
    public CursoRepository(PortalFiapContext context) : base(context)
    {
    }

    protected override IQueryable<Curso> Query() => base.Query().Include(c => c.Turmas);
}
