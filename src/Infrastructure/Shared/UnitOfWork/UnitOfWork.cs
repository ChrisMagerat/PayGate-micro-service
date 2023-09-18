using PayGateMicroService.Domain.Common;
using PayGateMicroService.Domain.Shared.Configuration;
using PayGateMicroService.Infrastructure.Persistence;

namespace PayGateMicroService.Infrastructure.Shared.UnitOfWork;

public class EntityFrameworkUnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _applicationDbContext;

    public EntityFrameworkUnitOfWork(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<int> CommitAsync()
    {
        return await _applicationDbContext.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        await _applicationDbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _applicationDbContext.Database.CurrentTransaction?.CommitAsync()!;
    }

    public async Task RollbackTransactionAsync()
    {
        await _applicationDbContext.Database.CurrentTransaction?.RollbackAsync()!;
    }
    
    public IEnumerable<DomainEvent> GetAndClearDomainEvents()
    {
        var domainEvents = _applicationDbContext.ChangeTracker.Entries<IAuditableEntity>()
            .SelectMany(entry => entry.Entity.DomainEvents)
            .ToList();

        var entities = _applicationDbContext.ChangeTracker.Entries<IAuditableEntity>().ToList();
        foreach (var entity in entities)
        {
            entity.Entity.DomainEvents.Clear();
        }

        return domainEvents;
    }
}