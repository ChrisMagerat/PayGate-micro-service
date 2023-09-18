using System.Linq.Expressions;
using PayGateMicroService.Domain.Common;
using PayGateMicroService.Domain.ExampleDomain.Entities;

namespace PayGateMicroService.Domain.ExampleDomain.RepositoryInterfaces;

public interface IExampleEntityRepository : IRepository
{
    Task<ExampleEntity?> GetExampleEntityByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<PaginationData<T>> GetExampleEntities<T>(PaginationOptions paginationOptions
        , Expression<Func<ExampleEntity, T>> mapping
        , CancellationToken cancellationToken, string? search = null) where T : class;
}