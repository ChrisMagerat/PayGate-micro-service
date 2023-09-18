using MediatR;
using Microsoft.Extensions.Logging;

namespace PayGateMicroService.Application.Shared.Contracts.Mediator;

public abstract class BaseAsyncRequestHandler<TRequest> : IRequestHandler<TRequest>
    where TRequest : IRequest
{
    private readonly ILogger<BaseAsyncRequestHandler<TRequest>> _logger;
    protected BaseAsyncRequestHandler(ILogger<BaseAsyncRequestHandler<TRequest>> logger)
    {
        _logger = logger;
    }
    public abstract Task Handle(TRequest request, CancellationToken cancellationToken);

    // Add any common properties or methods shared by your handlers here
    protected void Log(string message)
    {
        _logger.LogInformation(message);
    }
}

public abstract class BaseAsyncRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<BaseAsyncRequestHandler<TRequest, TResponse>> _logger;
    protected BaseAsyncRequestHandler(ILogger<BaseAsyncRequestHandler<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    
    // Add any common properties or methods shared by your handlers here
    protected void Log(string message)
    {
        _logger.LogInformation(message);
    }
}
