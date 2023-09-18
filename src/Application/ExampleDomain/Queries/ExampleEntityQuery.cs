using PayGateMicroService.Application.ExampleDomain.Dtos;
using PayGateMicroService.Application.ExampleDomain.Services;
using PayGateMicroService.Application.Shared.Contracts.Mediator;
using PayGateMicroService.Application.Shared.Contracts.Mediator.Implementations;
using Microsoft.Extensions.Logging;

namespace PayGateMicroService.Application.ExampleDomain.Queries;

public class ExampleEntityQuery : QueryBase<ExampleEntityDto>
{
    public ExampleEntityQuery(Guid entityId)
    {
        EntityId = entityId;
    }

    public Guid EntityId { get; set; }
}

public class ExampleEntityQueryHandler : BaseAsyncRequestHandler<ExampleEntityQuery, ExampleEntityDto>
{
    private readonly IExampleDomainService _domainService;
    public ExampleEntityQueryHandler(
        IExampleDomainService domainService,
        ILogger<ExampleEntityQueryHandler> logger)
        :base(logger)
    {
        _domainService = domainService;
    }
    
    public override async Task<ExampleEntityDto> Handle(ExampleEntityQuery request, CancellationToken cancellationToken)
    {
        return await _domainService.GetEntityById(request.EntityId, cancellationToken);
    }
}