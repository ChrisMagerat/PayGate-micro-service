using MediatR;
using PayGate.Application.Identity.Commands;

namespace PayGate.Application.Identity.DomainEventConsumers;

public class PasswordResetRequestedDomainEventConsumer
{
    // TODO: Implement IConsumer from mass transit
    private readonly IMediator _mediator;
    
    public PasswordResetRequestedDomainEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task Consume()
    {
        // TODO: replace Guid.NewGuid with userid when mass transit is working 
        await _mediator.Send(new SendPasswordResetCommand(Guid.NewGuid()));
    }
}