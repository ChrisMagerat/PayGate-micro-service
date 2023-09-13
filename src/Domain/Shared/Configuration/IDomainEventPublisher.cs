using PayGate.Domain.Common;

namespace PayGate.Domain.Shared.Configuration;

public interface IDomainEventPublisher
{
    Task PublishDomainEventsAsync(IEnumerable<DomainEvent> domainEvents);
}