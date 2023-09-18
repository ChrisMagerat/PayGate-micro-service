using PayGateMicroService.Application.Common;
using PayGateMicroService.Application.ExampleDomain.Dtos;
using PayGateMicroService.Application.ExampleDomain.Queries;
using PayGateMicroService.Application.ExampleDomain.Requests;
using PayGateMicroService.Domain.ExampleDomain.Entities;

namespace PayGateMicroService.Application.ExampleDomain.Services;

public interface IExampleDomainService
{
    Task<ExampleEntity> AddAsync(AddExampleEntityRequest request, CancellationToken cancellationToken);
    Task<PaginatedList<ExampleEntityDto>> GetEntities(ExampleEntitiesQuery request, CancellationToken cancellationToken);
    Task<ExampleEntityDto> GetEntityById(Guid entityId, CancellationToken cancellationToken);
    Task UpdateAsync(Guid id, UpdateExampleEntityRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(Guid entityId, CancellationToken cancellationToken);
}
