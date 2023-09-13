using ExampleProject.Application.Common;
using ExampleProject.Application.ExampleDomain.Dtos;
using ExampleProject.Application.ExampleDomain.Services;
using ExampleProject.Application.Shared.Contracts.Mediator;
using ExampleProject.Application.Shared.Contracts.Mediator.Implementations;
using ExampleProject.Domain.Common;
using Microsoft.Extensions.Logging;

namespace ExampleProject.Application.ExampleDomain.Queries;

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