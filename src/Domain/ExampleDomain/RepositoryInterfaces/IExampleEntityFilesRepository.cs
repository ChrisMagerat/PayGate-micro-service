using System.Linq.Expressions;
using PayGate.Domain.Common;
using PayGate.Domain.ExampleDomain.Entities;

namespace PayGate.Domain.ExampleDomain.RepositoryInterfaces;

public interface IExampleEntityFilesRepository : IRepository
{
    Task<PaginationData<T>> GetFilesByEntityIdAsync<T>(Guid entityId, Expression<Func<ExampleEntityFile, T>> mapping,
        PaginationOptions paginationOptions,
        CancellationToken cancellationToken) where T : class;

    Task<ExampleEntityFile?> GetBlobMetaDataByIdAsync(Guid fileId,
        CancellationToken cancellationToken);
}