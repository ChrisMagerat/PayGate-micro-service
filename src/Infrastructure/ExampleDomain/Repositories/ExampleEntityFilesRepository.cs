using System.Linq.Expressions;
using PayGateMicroService.Domain.Common;
using PayGateMicroService.Domain.ExampleDomain.Entities;
using PayGateMicroService.Domain.ExampleDomain.RepositoryInterfaces;
using PayGateMicroService.Infrastructure.Common;
using PayGateMicroService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace PayGateMicroService.Infrastructure.ExampleDomain.Repositories;

public class ExampleEntityFilesRepository : Repository, IExampleEntityFilesRepository
{
    public ExampleEntityFilesRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<PaginationData<T>> GetFilesByEntityIdAsync<T>(Guid entityId, Expression<Func<ExampleEntityFile, T>> mapping, PaginationOptions paginationOptions,
        CancellationToken cancellationToken) where T : class
    {
        var query = Context.ExampleEntityFiles
            .Where(file => file.ExampleEntityId == entityId);

        var mappedQuery = query.Select(mapping);

        return mappedQuery.GetPaginationDataAsync(paginationOptions, cancellationToken);
    }
    
    public async Task<ExampleEntityFile?> GetBlobMetaDataByIdAsync(Guid fileId,
        CancellationToken cancellationToken)
    {
        return await Context.ExampleEntities
            .SelectMany(entity => entity.Files)
            .Where(blob => blob.Id == fileId)
            .FirstOrDefaultAsync(cancellationToken);
    }
}