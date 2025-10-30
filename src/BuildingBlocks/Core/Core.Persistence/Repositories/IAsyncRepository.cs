using System.Linq.Expressions;
using Core.Persistence.Entities;

namespace Core.Persistence.Repositories;

public interface IAsyncRepository<TEntity, TId> where TEntity : Entity<TId>
{
    Task<bool> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<bool> AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<bool> UpdateRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<bool> DeleteRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null,
        bool ignoreQueryFilters = false, bool enableTracking = false, bool include = false,
        CancellationToken cancellationToken = default);

    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter, bool ignoreQueryFilters = false,
        bool enableTracking = false, bool include = false, CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? filter = null, bool include = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = false, CancellationToken cancellationToken = default);

    Task<TEntity?> GetByIdAsync(TId id, bool ignoreQueryFilters = false, bool enableTracking = false,
        bool include = false, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TEntity>> GetByIdsAsync(ICollection<TId>? ids, bool ignoreQueryFilters = false,
        bool enableTracking = false, bool include = false, CancellationToken cancellationToken = default);

    Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null, bool ignoreQueryFilters = false,
        bool include = false, CancellationToken cancellationToken = default);
}