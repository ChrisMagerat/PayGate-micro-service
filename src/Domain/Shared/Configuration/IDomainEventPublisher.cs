using PayGateMicroService.Domain.Common;

namespace PayGateMicroService.Domain.Shared.Configuration;

public interface IDomainEventPublisher
{
    Task PublishDomainEventsAsync(IEnumerable<DomainEvent> domainEvents);
}