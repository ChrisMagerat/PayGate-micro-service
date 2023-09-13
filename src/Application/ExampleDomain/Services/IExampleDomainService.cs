using ExampleProject.Application.Common;
using ExampleProject.Application.ExampleDomain.Dtos;
using ExampleProject.Application.ExampleDomain.Queries;
using ExampleProject.Application.ExampleDomain.Requests;
using ExampleProject.Domain.ExampleDomain.Entities;

namespace ExampleProject.Application.ExampleDomain.Services;

public interface IExampleDomainService
{
    Task<ExampleEntity> AddAsync(AddExampleEntityRequest request, CancellationToken cancellationToken);
    Task<PaginatedList<ExampleEntityDto>> GetEntities(ExampleEntitiesQuery request, CancellationToken cancellationToken);
    Task<ExampleEntityDto> GetEntityById(Guid entityId, CancellationToken cancellationToken);
    Task UpdateAsync(Guid id, UpdateExampleEntityRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(Guid entityId, CancellationToken cancellationToken);
}
