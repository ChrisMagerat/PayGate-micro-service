using MediatR;

namespace PayGate.Application.Shared.Contracts.Mediator.Interfaces;

public interface ICommand : IRequest
{ 
    Guid Id { get; }
}