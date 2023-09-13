using ExampleProject.Domain.Common;

namespace ExampleProject.Domain.Shared.Configuration;

public interface IUnitOfWork
{
    Task<int> CommitAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    IEnumerable<DomainEvent> GetAndClearDomainEvents();
}