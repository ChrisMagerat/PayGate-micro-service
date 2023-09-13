using ExampleProject.Application.Shared.Contracts.Mediator.Interfaces;
using ExampleProject.Domain.Shared.Configuration;
using MediatR;
using Prometheus;

namespace ExampleProject.Application.Shared.Contracts.Behaviors;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : notnull
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDomainEventPublisher _domainEventPublisher;
    public TransactionBehavior(IUnitOfWork unitOfWork, IDomainEventPublisher domainEventPublisher)
    {
        _unitOfWork = unitOfWork;
        _domainEventPublisher = domainEventPublisher;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        
            if (request is not ICommandBase)
            {
                return await PerformNextOperationAsync(next, request);
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var response = await PerformNextOperationAsync(next, request);

                var domainEvents = _unitOfWork.GetAndClearDomainEvents();

                await _unitOfWork.CommitTransactionAsync();
                await _unitOfWork.CommitAsync();
            
                // TODO: Uncomment when domainEventPublisher has been implemented
                // This currently has a throw NotImplemented() method 
                //await _domainEventPublisher.PublishDomainEventsAsync(domainEvents);

                return response;
            }
            catch(Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        
    }
    
    private async Task<TResponse> PerformNextOperationAsync(RequestHandlerDelegate<TResponse> next, TRequest request)
    {
        var duration 
            = Metrics.CreateSummary(request.GetType().Name, "Duration of my operation in seconds");

        using (duration.NewTimer())
        {
            return await next();
        }
    }   
    
}