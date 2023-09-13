using Microsoft.Extensions.Logging;
using PayGate.Application.Common;
using PayGate.Application.ExampleDomain.Dtos;
using PayGate.Application.ExampleDomain.Services;
using PayGate.Application.Shared.Contracts.Mediator;
using PayGate.Application.Shared.Contracts.Mediator.Implementations;
using PayGate.Domain.Common;

namespace PayGate.Application.ExampleDomain.Queries;

public class ExampleEntitiesQuery : QueryBase<PaginatedList<ExampleEntityDto>>
{
    public ExampleEntitiesQuery(PaginationOptions pagination, string? search = null)
    {
        Search = search;
        Pagination = pagination;
    }
    public string? Search { get; set; }
    public PaginationOptions Pagination { get; set; }
}

public class ExampleEntitiesQueryHandler : BaseAsyncRequestHandler<ExampleEntitiesQuery, PaginatedList<ExampleEntityDto>>
{
    private readonly IExampleDomainService _exampleDomainService;

    public ExampleEntitiesQueryHandler(
        ILogger<ExampleEntitiesQueryHandler> logger, IExampleDomainService exampleDomainService)
        :base(logger)
    {
        _exampleDomainService = exampleDomainService;
    }
    
    public override async Task<PaginatedList<ExampleEntityDto>> Handle(ExampleEntitiesQuery request, CancellationToken cancellationToken)
    {
        return await _exampleDomainService.GetEntities(request, cancellationToken);
    }
}