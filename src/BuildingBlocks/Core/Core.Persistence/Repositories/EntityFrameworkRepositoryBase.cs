using System.Linq.Expressions;
using Core.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Persistence.Repositories;

public class EntityFrameworkRepositoryBase<TEntity, TId, TContext>(TContext context)
    : IAsyncRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TContext : DbContext
{
    public async Task<bool> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        await context.Set<TEntity>().AddAsync(entity, cancellationToken);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
    {
        if (entities is null || entities.Count == 0) throw new ArgumentNullException(nameof(entities));
        await context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        entity.UpdatedTime = DateTime.UtcNow;
        context.Set<TEntity>().Update(entity);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> UpdateRangeAsync(ICollection<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entities);
        if (entities.Count == 0) throw new ArgumentException("Entity collection cannot be empty", nameof(entities));
        var now = DateTime.UtcNow;
        foreach (var entity in entities) entity.UpdatedTime = now;
        context.Set<TEntity>().UpdateRange(entities);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        context.Set<TEntity>().Remove(entity);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteRangeAsync(ICollection<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        if (entities is null || entities.Count == 0) throw new ArgumentNullException(nameof(entities));
        context.Set<TEntity>().RemoveRange(entities);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        bool ignoreQueryFilters = false,
        bool enableTracking = false,
        bool include = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = context.Set<TEntity>();
        if (filter is not null) query = query.Where(filter);
        if (ignoreQueryFilters) query = query.IgnoreQueryFilters();
        if (!enableTracking) query = query.AsNoTracking();
        if (!include) query = query.IgnoreAutoIncludes();
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> filter,
        bool ignoreQueryFilters = false,
        bool enableTracking = false,
        bool include = false,
        CancellationToken cancellationToken = default)
    {
        var query = context.Set<TEntity>().Where(filter);
        if (ignoreQueryFilters) query = query.IgnoreQueryFilters();
        if (!enableTracking) query = query.AsNoTracking();
        if (!include) query = query.IgnoreAutoIncludes();
        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        bool ignoreQueryFilters = false,
        bool enableTracking = false,
        bool include = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = context.Set<TEntity>();
        if (filter is not null) query = query.Where(filter);
        if (ignoreQueryFilters) query = query.IgnoreQueryFilters();
        if (!enableTracking) query = query.AsNoTracking();
        if (!include) query = query.IgnoreAutoIncludes();
        return await query.AnyAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(
        TId id,
        bool ignoreQueryFilters = false,
        bool enableTracking = false,
        bool include = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = context.Set<TEntity>();
        if (ignoreQueryFilters) query = query.IgnoreQueryFilters();
        if (!enableTracking) query = query.AsNoTracking();
        if (!include) query = query.IgnoreAutoIncludes();
        return await query.FirstOrDefaultAsync(x => EqualityComparer<TId>.Default.Equals(x.Id, id), cancellationToken);
    }

    public async Task<IReadOnlyList<TEntity>> GetByIdsAsync(
        ICollection<TId>? ids,
        bool ignoreQueryFilters = false,
        bool enableTracking = false,
        bool include = false,
        CancellationToken cancellationToken = default)
    {
        if (ids is null || ids.Count == 0) throw new ArgumentNullException(nameof(ids));
        IQueryable<TEntity> query = context.Set<TEntity>();
        if (ignoreQueryFilters) query = query.IgnoreQueryFilters();
        if (!enableTracking) query = query.AsNoTracking();
        if (!include) query = query.IgnoreAutoIncludes();
        query = query.Where(x => ids.Contains(x.Id));
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        bool ignoreQueryFilters = false,
        bool include = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = context.Set<TEntity>();

        if (filter is not null) query = query.Where(filter);
        if (ignoreQueryFilters) query = query.IgnoreQueryFilters();
        if (!include) query = query.IgnoreAutoIncludes();
        return await query.CountAsync(cancellationToken);
    }
}