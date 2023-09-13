using MediatR;

namespace PayGate.Application.Shared.Contracts.Mediator.Interfaces;

public interface IQuery<out TResult> : IRequest<TResult>, IRequestBase
{
}