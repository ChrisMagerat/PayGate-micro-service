using MediatR;

namespace ExampleProject.Application.Shared.Contracts.Mediator.Interfaces;

public interface ICommandWithResult<out TResult> : IRequest<TResult>
{
    Guid Id { get; }
}