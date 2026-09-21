using PortalFiap.Domain.Commom;

namespace PortalFIAP.Application.Interfaces.Repositories;

/// <summary>
/// Contrato genérico de persistência para qualquer entidade do domínio.
/// A exclusão é lógica (<see cref="BaseEntity.Deactivate"/>) e as consultas retornam apenas registros ativos.
/// </summary>
/// <typeparam name="T">Entidade do domínio, derivada de <see cref="BaseEntity"/>.</typeparam>
public interface IRepository<T> where T : BaseEntity
{
    /// <summary>Lista todas as entidades ativas.</summary>
    Task<IReadOnlyList<T>> GetAllAsync();

    /// <summary>Busca uma entidade ativa pelo Id; retorna <c>null</c> se não existir.</summary>
    Task<T?> GetByIdAsync(Guid id);

    /// <summary>Verifica se existe uma entidade ativa com o Id informado.</summary>
    Task<bool> ExistsByIdAsync(Guid id);

    /// <summary>Persiste uma nova entidade.</summary>
    Task AddAsync(T entity);

    /// <summary>Persiste as alterações de uma entidade existente.</summary>
    Task UpdateAsync(T entity);

    /// <summary>Desativa (exclusão lógica) a entidade; retorna <c>false</c> se não existir.</summary>
    Task<bool> DeleteAsync(Guid id);
}
