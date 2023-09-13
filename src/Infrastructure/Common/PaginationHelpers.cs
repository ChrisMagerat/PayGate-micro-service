using ExampleProject.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ExampleProject.Infrastructure.Common;

public static class PaginationHelpers
{
    public static async Task<PaginationData<TEntity>> GetPaginationDataAsync<TEntity>(this IQueryable<TEntity> query,
        PaginationOptions paginationOptions,
        CancellationToken cancellationToken) where TEntity : class
    {
        query = query.AsNoTracking();
        var count = await query.CountAsync(cancellationToken);
        var items = await query.Skip((paginationOptions.PageNumber - 1) * paginationOptions.PageSize)
            .Take(paginationOptions.PageSize).ToListAsync(cancellationToken);

        return new PaginationData<TEntity>(items, count, paginationOptions);
    }
}
