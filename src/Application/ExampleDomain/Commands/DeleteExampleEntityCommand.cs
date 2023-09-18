using PayGateMicroService.Application.ExampleDomain.Services;
using PayGateMicroService.Application.Shared.Contracts.Mediator;
using PayGateMicroService.Application.Shared.Contracts.Mediator.Implementations;
using Microsoft.Extensions.Logging;

namespace PayGateMicroService.Application.ExampleDomain.Commands;

public class DeleteExampleEntityCommand : CommandBase
{
    public DeleteExampleEntityCommand(Guid entityToDeleteId)
    {
        EntityToDeleteId = entityToDeleteId;
    }

    public Guid EntityToDeleteId { get; set; }
}

public class DeleteExampleEntityCommandHandler : BaseAsyncRequestHandler<DeleteExampleEntityCommand>
{
    private readonly IExampleDomainService _domainService;

    public DeleteExampleEntityCommandHandler(
        IExampleDomainService domainService, 
        ILogger<DeleteExampleEntityCommandHandler> logger) 
        : base(logger)
    {
        _domainService = domainService;
    }

    public override async Task Handle(DeleteExampleEntityCommand request, CancellationToken cancellationToken)
    {
        await _domainService.DeleteAsync(request.EntityToDeleteId, cancellationToken);
    }
}