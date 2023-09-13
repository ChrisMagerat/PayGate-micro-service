using System.Linq.Expressions;
using ExampleProject.Domain.Common;
using ExampleProject.Domain.ExampleDomain.Entities;

namespace ExampleProject.Domain.ExampleDomain.RepositoryInterfaces;

public interface IExampleEntityRepository : IRepository
{
    Task<ExampleEntity?> GetExampleEntityByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<PaginationData<T>> GetExampleEntities<T>(PaginationOptions paginationOptions
        , Expression<Func<ExampleEntity, T>> mapping
        , CancellationToken cancellationToken, string? search = null) where T : class;
}