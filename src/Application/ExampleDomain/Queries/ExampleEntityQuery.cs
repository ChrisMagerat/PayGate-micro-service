using Microsoft.Extensions.Logging;
using PayGate.Application.ExampleDomain.Dtos;
using PayGate.Application.ExampleDomain.Services;
using PayGate.Application.Shared.Contracts.Mediator;
using PayGate.Application.Shared.Contracts.Mediator.Implementations;

namespace PayGate.Application.ExampleDomain.Queries;

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