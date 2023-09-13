using ExampleProject.Domain.Common;

namespace ExampleProject.Application.Common;

public static class PaginationHelpers
{
    public static PaginatedList<TEntity> ToPaginatedList<TEntity>(this PaginationData<TEntity> paginationData)
    {
        return new PaginatedList<TEntity>(paginationData.Items, paginationData.Count, paginationData.PaginationOptions.PageNumber, paginationData.PaginationOptions.PageSize);
    }

    public static PaginatedList<TMapped> ToPaginatedList<TEntity, TMapped>(this PaginationData<TEntity> paginationData, IEnumerable<TMapped> items)
    {
        return new PaginatedList<TMapped>(items.ToList(), paginationData.Count, paginationData.PaginationOptions.PageNumber, paginationData.PaginationOptions.PageSize);
    }
}