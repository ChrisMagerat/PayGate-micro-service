using MediatR;

namespace PayGateMicroService.Application.Shared.Contracts.Mediator.Interfaces;

public interface ICommand : IRequest
{ 
    Guid Id { get; }
}