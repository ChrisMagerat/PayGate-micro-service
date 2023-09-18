using PayGateMicroService.Application.Identity.Commands;
using MediatR;

namespace PayGateMicroService.Application.Identity.DomainEventConsumers;

public class UserSignedUpDomainEventConsumer
{
    // TODO: Implement IConsumer from mass transit
    private readonly IMediator _mediator;
    
    public UserSignedUpDomainEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task Consume()
    {
        // TODO: replace Guid.NewGuid with userid when mass transit is working 
        await _mediator.Send(new SignUpEmailVerificationCommand(Guid.NewGuid()));
    }
}