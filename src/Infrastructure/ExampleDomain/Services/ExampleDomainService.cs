using PayGateMicroService.Application.Common;
using PayGateMicroService.Application.ExampleDomain.Dtos;
using PayGateMicroService.Application.ExampleDomain.Queries;
using PayGateMicroService.Application.ExampleDomain.Requests;
using PayGateMicroService.Application.ExampleDomain.Services;
using PayGateMicroService.Domain.ExampleDomain.Entities;
using PayGateMicroService.Domain.ExampleDomain.RepositoryInterfaces;
using SendGrid.Helpers.Errors.Model;

namespace PayGateMicroService.Infrastructure.ExampleDomain.Services;

public class ExampleDomainService : IExampleDomainService
{
    private readonly IExampleEntityRepository _exampleEntityRepository;
    
    public ExampleDomainService(IExampleEntityRepository exampleEntityRepository)
    {
        _exampleEntityRepository = exampleEntityRepository;
    }
    
    public async Task<ExampleEntity> AddAsync(AddExampleEntityRequest request, CancellationToken cancellationToken)
    {
        var exampleEntity = new ExampleEntity(request.EntityName, request.RegistrationDate);
        return await _exampleEntityRepository.AddAsync(exampleEntity, cancellationToken);
    }

    public async Task<PaginatedList<ExampleEntityDto>> GetEntities(ExampleEntitiesQuery request, CancellationToken cancellationToken)
    {
        var exampleEntities = await _exampleEntityRepository.GetExampleEntities(request.Pagination
            , exampleEntity => new ExampleEntityDto(exampleEntity.Id, exampleEntity.Name, exampleEntity.RegistrationDate)
            , cancellationToken
            , request.Search) ?? throw new NotFoundException("No exampleEntities found");

        return exampleEntities.ToPaginatedList();
    }

    public async Task<ExampleEntityDto> GetEntityById(Guid entityId, CancellationToken cancellationToken)
    {
        var exampleEntity = await _exampleEntityRepository.GetExampleEntityByIdAsync(entityId, cancellationToken) 
                            ?? throw new NotFoundException("No exampleEntities found");
        
        return new ExampleEntityDto(exampleEntity.Id, exampleEntity.Name, exampleEntity.RegistrationDate);
    }

    public async Task UpdateAsync(Guid id, UpdateExampleEntityRequest request, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _exampleEntityRepository.GetExampleEntityByIdAsync(id, cancellationToken) 
                             ?? throw new NotFoundException("No exampleEntities found");

        entityToUpdate.RegistrationDate = request.RegistrationDate;
        entityToUpdate.Name = request.EntityName;
        _exampleEntityRepository.Update(entityToUpdate);
    }

    public async Task DeleteAsync(Guid entityId, CancellationToken cancellationToken)
    {
        var entityToDelete = await _exampleEntityRepository.GetExampleEntityByIdAsync(entityId, cancellationToken) 
                             ?? throw new NotFoundException("No exampleEntities found");

        entityToDelete.Delete();
        _exampleEntityRepository.Delete(entityToDelete);
    }
}