using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PayGate.Domain.Common;
using PayGate.Domain.ExampleDomain.Entities;
using PayGate.Domain.ExampleDomain.RepositoryInterfaces;
using PayGate.Infrastructure.Common;
using PayGate.Infrastructure.Persistence;

namespace PayGate.Infrastructure.ExampleDomain.Repositories;

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