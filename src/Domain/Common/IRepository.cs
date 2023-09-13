namespace ExampleProject.Domain.Common;

public interface IRepository
{
    Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : AuditableEntity;
    Task<IEnumerable<TEntity>> AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken) where TEntity : AuditableEntity;
    TEntity Update<TEntity>(TEntity entity) where TEntity : AuditableEntity;

    IEnumerable<TEntity> UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : AuditableEntity;

    void Delete<TEntity>(TEntity entity) where TEntity : AuditableEntity;

    void DeleteRange<TEntity>(List<TEntity> entities) where TEntity : AuditableEntity;
}
