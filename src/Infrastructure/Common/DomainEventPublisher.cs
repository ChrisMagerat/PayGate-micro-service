using ExampleProject.Domain.Common;
using ExampleProject.Domain.Shared.Configuration;

namespace ExampleProject.Infrastructure.Common;

public class DomainEventPublisher : IDomainEventPublisher
{
    public Task PublishDomainEventsAsync(IEnumerable<DomainEvent> domainEvents)
    {
        throw new NotImplementedException();
    }
}