using PayGateMicroService.Application.ExampleDomain.Requests;
using PayGateMicroService.Application.ExampleDomain.Services;
using PayGateMicroService.Application.Shared.Contracts.Mediator;
using PayGateMicroService.Application.Shared.Contracts.Mediator.Implementations;
using Microsoft.Extensions.Logging;

namespace PayGateMicroService.Application.ExampleDomain.Commands;

public class ExampleEntityCommand : CommandBase
{
    public ExampleEntityCommand(AddExampleEntityRequest request)
    {
        Request = request;
    }
    public AddExampleEntityRequest Request { get;  }
}

public class AddExampleEntityRequestHandler : BaseAsyncRequestHandler<ExampleEntityCommand>
{
    private readonly IExampleDomainService _domainService;

    public AddExampleEntityRequestHandler(
        IExampleDomainService domainService,
        ILogger<AddExampleEntityRequestHandler> logger) 
        : base(logger)
    {
        _domainService = domainService;
    }
    
    public override Task Handle(ExampleEntityCommand request, CancellationToken cancellationToken)
    {
        return _domainService.AddAsync(request.Request, cancellationToken);
    }
}