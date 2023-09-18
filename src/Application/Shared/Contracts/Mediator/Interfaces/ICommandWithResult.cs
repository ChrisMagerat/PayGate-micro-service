using MediatR;

namespace PayGateMicroService.Application.Shared.Contracts.Mediator.Interfaces;

public interface ICommandWithResult<out TResult> : IRequest<TResult>
{
    Guid Id { get; }
}