using PayGateMicroService.Application.Common;
using PayGateMicroService.Application.ExampleDomain.Dtos;
using PayGateMicroService.Application.ExampleDomain.Services;
using PayGateMicroService.Application.Shared.Contracts.Mediator;
using PayGateMicroService.Application.Shared.Contracts.Mediator.Implementations;
using PayGateMicroService.Domain.Common;
using Microsoft.Extensions.Logging;

namespace PayGateMicroService.Application.ExampleDomain.Queries;

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