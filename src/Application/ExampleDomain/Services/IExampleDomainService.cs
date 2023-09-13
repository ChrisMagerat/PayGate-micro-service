using PayGate.Application.Common;
using PayGate.Application.ExampleDomain.Dtos;
using PayGate.Application.ExampleDomain.Queries;
using PayGate.Application.ExampleDomain.Requests;
using PayGate.Domain.ExampleDomain.Entities;

namespace PayGate.Application.ExampleDomain.Services;

public interface IExampleDomainService
{
    Task<ExampleEntity> AddAsync(AddExampleEntityRequest request, CancellationToken cancellationToken);
    Task<PaginatedList<ExampleEntityDto>> GetEntities(ExampleEntitiesQuery request, CancellationToken cancellationToken);
    Task<ExampleEntityDto> GetEntityById(Guid entityId, CancellationToken cancellationToken);
    Task UpdateAsync(Guid id, UpdateExampleEntityRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(Guid entityId, CancellationToken cancellationToken);
}
