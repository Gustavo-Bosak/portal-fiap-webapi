using Microsoft.EntityFrameworkCore;
using PortalFiap.Domain.Commom;
using PortalFIAP.Application.Interfaces.Repositories;

namespace PortalFiap.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implementação EF Core de <see cref="IRepository{T}"/>. Repositórios específicos herdam desta
/// classe e sobrescrevem <see cref="Query"/> para incluir relacionamentos.
/// </summary>
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly PortalFiapContext Context;

    public Repository(PortalFiapContext context)
    {
        Context = context;
    }

    /// <summary>Consulta base (apenas registros ativos). Sobrescreva para adicionar <c>Include</c>.</summary>
    protected virtual IQueryable<T> Query() => Context.Set<T>().Where(e => e.Active);

    public virtual async Task<IReadOnlyList<T>> GetAllAsync() =>
        await Query().AsNoTracking().ToListAsync();

    public virtual async Task<T?> GetByIdAsync(Guid id) =>
        await Query().FirstOrDefaultAsync(e => e.Id == id);

    public virtual async Task<bool> ExistsByIdAsync(Guid id) =>
        await Context.Set<T>().AnyAsync(e => e.Id == id && e.Active);

    public virtual async Task AddAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(T entity)
    {
        Context.Set<T>().Update(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await Context.Set<T>().FirstOrDefaultAsync(e => e.Id == id && e.Active);
        if (entity is null) return false;

        entity.Deactivate();
        await Context.SaveChangesAsync();
        return true;
    }
}
