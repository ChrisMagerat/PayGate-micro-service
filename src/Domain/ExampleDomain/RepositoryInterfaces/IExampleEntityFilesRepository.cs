using System.Linq.Expressions;
using ExampleProject.Domain.Common;
using ExampleProject.Domain.ExampleDomain.Entities;

namespace ExampleProject.Domain.ExampleDomain.RepositoryInterfaces;

public interface IExampleEntityFilesRepository : IRepository
{
    Task<PaginationData<T>> GetFilesByEntityIdAsync<T>(Guid entityId, Expression<Func<ExampleEntityFile, T>> mapping,
        PaginationOptions paginationOptions,
        CancellationToken cancellationToken) where T : class;

    Task<ExampleEntityFile?> GetBlobMetaDataByIdAsync(Guid fileId,
        CancellationToken cancellationToken);
}