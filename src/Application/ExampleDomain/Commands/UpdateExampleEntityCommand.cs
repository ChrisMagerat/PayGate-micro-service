using ExampleProject.Application.ExampleDomain.Requests;
using ExampleProject.Application.ExampleDomain.Services;
using ExampleProject.Application.Shared.Contracts.Mediator;
using ExampleProject.Application.Shared.Contracts.Mediator.Implementations;
using Microsoft.Extensions.Logging;

namespace ExampleProject.Application.ExampleDomain.Commands;

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