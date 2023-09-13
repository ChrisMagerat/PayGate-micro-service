using PayGate.Domain.Common;
using PayGate.Infrastructure.Common.Exceptions;
using PayGate.Infrastructure.Persistence;

namespace PayGate.Infrastructure.Common;

public abstract class Repository: IRepository
{
    protected readonly ApplicationDbContext Context;

    protected Repository(ApplicationDbContext context)
    {
        Context = context;
    }

    public async Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : AuditableEntity
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "entity must not be null");
        }

        try
        {
            await Context.AddAsync(entity, cancellationToken);

            return entity;
        }
        catch (Exception ex)
        {
            throw new DatabaseOperationException($"{nameof(entity)} could not be saved", ex);
        }
    }

    public async Task<IEnumerable<TEntity>> AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken) where TEntity : AuditableEntity
    {
        if (entities == null)
        {
            throw new ArgumentNullException(nameof(entities), "entities must not be null");
        }

        try
        {
            var entriesAdded = entities.ToList();
            await Context.AddRangeAsync(entriesAdded, cancellationToken);
            return entriesAdded;
        }
        catch (Exception ex)
        {
            throw new DatabaseOperationException($"{nameof(entities)} could not be saved", ex);
        }
    }

    public TEntity Update<TEntity>(TEntity entity) where TEntity : AuditableEntity
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "entities must not be null");
        }
        try
        {
            Context.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            throw new DatabaseOperationException($"{nameof(entity)} could not be saved", ex);
        }
    }

    public IEnumerable<TEntity> UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : AuditableEntity
    {
        if (entities == null)
        {
            throw new ArgumentNullException(nameof(entities), "entities must not be null");
        }

        try
        {
            var updatedEntities = entities.ToList();
            Context.UpdateRange(updatedEntities);

            return updatedEntities;
        }
        catch (Exception ex)
        {
            throw new DatabaseOperationException($"{nameof(entities)} could not be saved", ex);
        }
    }

    public void Delete<TEntity>(TEntity entity) where TEntity : AuditableEntity
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "entities must not be null");
        }
        try
        {
            Context.Remove(entity);
        }
        catch (Exception ex)
        {
            throw new DatabaseOperationException($"{nameof(entity)} could not be saved", ex);
        }
    }

    public void DeleteRange<TEntity>(List<TEntity> entities) where TEntity : AuditableEntity
    {
        if (entities == null)
        {
            throw new ArgumentNullException(nameof(entities), "entities must not be null");
        }

        try
        {
            var updatedEntities = entities.ToList();
            Context.RemoveRange(updatedEntities);
        }
        catch (Exception ex)
        {
            throw new DatabaseOperationException($"{nameof(entities)} could not be saved", ex);
        }
    }
}
