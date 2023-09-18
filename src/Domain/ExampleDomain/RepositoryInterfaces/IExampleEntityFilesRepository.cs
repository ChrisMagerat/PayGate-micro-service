using System.Linq.Expressions;
using PayGateMicroService.Domain.Common;
using PayGateMicroService.Domain.ExampleDomain.Entities;

namespace PayGateMicroService.Domain.ExampleDomain.RepositoryInterfaces;

public interface IExampleEntityFilesRepository : IRepository
{
    Task<PaginationData<T>> GetFilesByEntityIdAsync<T>(Guid entityId, Expression<Func<ExampleEntityFile, T>> mapping,
        PaginationOptions paginationOptions,
        CancellationToken cancellationToken) where T : class;

    Task<ExampleEntityFile?> GetBlobMetaDataByIdAsync(Guid fileId,
        CancellationToken cancellationToken);
}