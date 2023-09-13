using MediatR;

namespace ExampleProject.Application.Shared.Contracts.Mediator.Interfaces;

public interface ICommand : IRequest
{ 
    Guid Id { get; }
}