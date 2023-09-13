using MediatR;

namespace PayGate.Application.Shared.Contracts.Mediator.Interfaces;

public interface ICommandWithResult<out TResult> : IRequest<TResult>
{
    Guid Id { get; }
}