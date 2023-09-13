namespace ExampleProject.Domain.Common;

public class PaginationData<TEntity>
{
    public List<TEntity> Items { get; set; }
    public int Count { get; set; }
    public PaginationOptions PaginationOptions { get; set; }

    public PaginationData(List<TEntity> items, int count, PaginationOptions paginationOptions)
    {
        Items = items;
        Count = count;
        PaginationOptions = paginationOptions;
    }
}