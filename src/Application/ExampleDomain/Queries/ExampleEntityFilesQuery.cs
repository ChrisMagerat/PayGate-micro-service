using PayGateMicroService.Application.Common;
using PayGateMicroService.Application.ExampleDomain.Dtos;
using PayGateMicroService.Application.Shared.Contracts.Mediator;
using PayGateMicroService.Application.Shared.Contracts.Mediator.Implementations;
using PayGateMicroService.Application.Shared.Services;
using PayGateMicroService.Domain.Common;
using PayGateMicroService.Domain.ExampleDomain.Entities;
using PayGateMicroService.Domain.ExampleDomain.RepositoryInterfaces;
using Microsoft.Extensions.Logging;

namespace PayGateMicroService.Application.ExampleDomain.Queries;

public class ExampleEntityFilesQuery : QueryBase<PaginatedList<ExampleEntityFileDto>>
{
    public ExampleEntityFilesQuery(Guid exampleEntityId, PaginationOptions pagination, string? search)
    {
        ExampleEntityId = exampleEntityId;
        Pagination = pagination;
        Search = search;
    }

    public string? Search { get; }
    public PaginationOptions Pagination { get; }
    public Guid ExampleEntityId { get; }
}

public class ExampleEntityFilesQueryHandler : BaseAsyncRequestHandler<ExampleEntityFilesQuery, PaginatedList<ExampleEntityFileDto>>
{
    private readonly IExampleEntityFilesRepository _exampleEntityFilesRepository;
    
    public ExampleEntityFilesQueryHandler(ILogger<ExampleEntityFilesQueryHandler> logger
        , IExampleEntityFilesRepository exampleEntityFilesRepository) 
        : base(logger)
    {
        _exampleEntityFilesRepository = exampleEntityFilesRepository;
    }

    public override async Task<PaginatedList<ExampleEntityFileDto>> Handle(ExampleEntityFilesQuery request, CancellationToken cancellationToken)
    {
        var files = await _exampleEntityFilesRepository.GetFilesByEntityIdAsync(
            request.ExampleEntityId
            , file => new ExampleEntityFileDto(file.Id, file.Metadata, file.Description)
            , request.Pagination
            , cancellationToken);

        return files.ToPaginatedList();
    }
}