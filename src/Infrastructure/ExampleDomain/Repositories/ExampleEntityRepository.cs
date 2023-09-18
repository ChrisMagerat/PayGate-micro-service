using System.Linq.Expressions;
using PayGateMicroService.Domain.Common;
using PayGateMicroService.Domain.ExampleDomain.Entities;
using PayGateMicroService.Domain.ExampleDomain.RepositoryInterfaces;
using PayGateMicroService.Infrastructure.Common;
using PayGateMicroService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace PayGateMicroService.Infrastructure.ExampleDomain.Repositories;

public class ExampleEntityRepository : Repository, IExampleEntityRepository
{
    public ExampleEntityRepository(ApplicationDbContext context) 
        : base(context)
    {
    }
    
    public async Task<ExampleEntity?> GetExampleEntityByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Context.ExampleEntities
            .Where(exampleEntity => exampleEntity.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<PaginationData<T>> GetExampleEntities<T>(
        PaginationOptions paginationOptions,
        Expression<Func<ExampleEntity, T>> mapping,
        CancellationToken cancellationToken, string? search = null) where T : class
    {
        var query = Context.ExampleEntities.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(exampleEntity => exampleEntity.Name.Contains(search));    
        }

        var mappedQuery = query.Select(mapping);
        return mappedQuery.GetPaginationDataAsync(paginationOptions, cancellationToken);
    }
}