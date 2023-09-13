using System.Linq.Expressions;
using ExampleProject.Domain.Common;
using ExampleProject.Domain.ExampleDomain.Entities;
using ExampleProject.Domain.ExampleDomain.RepositoryInterfaces;
using ExampleProject.Infrastructure.Common;
using ExampleProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace ExampleProject.Infrastructure.ExampleDomain.Repositories;

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