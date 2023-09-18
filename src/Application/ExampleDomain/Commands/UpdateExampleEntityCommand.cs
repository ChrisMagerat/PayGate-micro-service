using PayGateMicroService.Application.ExampleDomain.Requests;
using PayGateMicroService.Application.ExampleDomain.Services;
using PayGateMicroService.Application.Shared.Contracts.Mediator;
using PayGateMicroService.Application.Shared.Contracts.Mediator.Implementations;
using Microsoft.Extensions.Logging;

namespace PayGateMicroService.Application.ExampleDomain.Commands;

public class UpdateExampleEntityCommand : CommandBase
{
    public UpdateExampleEntityCommand(UpdateExampleEntityRequest request, Guid id)
    {
        Request = request;
        Id = id;
    }

    public UpdateExampleEntityRequest Request { get; set; }
    public Guid Id { get; set; }
}

public class UpdateExampleEntityCommandHandler : BaseAsyncRequestHandler<UpdateExampleEntityCommand>
{
    private readonly IExampleDomainService _domainService;

    public UpdateExampleEntityCommandHandler(
        IExampleDomainService domainService,
        ILogger<UpdateExampleEntityCommandHandler> logger) 
        : base(logger)
    {
        _domainService = domainService;
    }
    public override async Task Handle(UpdateExampleEntityCommand request, CancellationToken cancellationToken)
    {
        await _domainService.UpdateAsync(request.Id, request.Request, cancellationToken);
    }
}