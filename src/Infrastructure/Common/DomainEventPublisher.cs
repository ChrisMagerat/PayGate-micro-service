using PayGateMicroService.Domain.Common;
using PayGateMicroService.Domain.Shared.Configuration;

namespace PayGateMicroService.Infrastructure.Common;

public class DomainEventPublisher : IDomainEventPublisher
{
    public Task PublishDomainEventsAsync(IEnumerable<DomainEvent> domainEvents)
    {
        throw new NotImplementedException();
    }
}