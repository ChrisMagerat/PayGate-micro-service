using PayGate.Domain.Common;
using PayGate.Domain.Shared.Configuration;

namespace PayGate.Infrastructure.Identity.Configuration;

public class DomainEventPublisher : IDomainEventPublisher
{
    //private readonly IBus _bus;
    
    public DomainEventPublisher()
    {
        // _bus = bus;
    }

    public async Task PublishDomainEventsAsync(IEnumerable<DomainEvent> domainEvents)
    {
        // TODO: implement mass transit
        throw new NotImplementedException();
        foreach (var domainEvent in domainEvents)
        {
            //await _bus.Publish(domainEvent.GetType(), domainEvent);
        }
    }
}