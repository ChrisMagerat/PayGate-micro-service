using System.Linq.Expressions;
using PayGate.Domain.Common;
using PayGate.Domain.ExampleDomain.Entities;

namespace PayGate.Domain.ExampleDomain.RepositoryInterfaces;

public interface IExampleEntityRepository : IRepository
{
    Task<ExampleEntity?> GetExampleEntityByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<PaginationData<T>> GetExampleEntities<T>(PaginationOptions paginationOptions
        , Expression<Func<ExampleEntity, T>> mapping
        , CancellationToken cancellationToken, string? search = null) where T : class;
}