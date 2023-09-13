using PayGate.Domain.Common;
using PayGate.Domain.Shared.Configuration;

namespace PayGate.Infrastructure.Common;

public class DomainEventPublisher : IDomainEventPublisher
{
    public Task PublishDomainEventsAsync(IEnumerable<DomainEvent> domainEvents)
    {
        throw new NotImplementedException();
    }
}