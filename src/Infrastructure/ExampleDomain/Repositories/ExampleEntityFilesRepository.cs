using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PayGate.Domain.Common;
using PayGate.Domain.ExampleDomain.Entities;
using PayGate.Domain.ExampleDomain.RepositoryInterfaces;
using PayGate.Infrastructure.Common;
using PayGate.Infrastructure.Persistence;

namespace PayGate.Infrastructure.ExampleDomain.Repositories;

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