using ExampleProject.Domain.Common;

namespace ExampleProject.Domain.Shared.Configuration;

public interface IDomainEventPublisher
{
    Task PublishDomainEventsAsync(IEnumerable<DomainEvent> domainEvents);
}