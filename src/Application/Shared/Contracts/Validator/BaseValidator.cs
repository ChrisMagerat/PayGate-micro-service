using ExampleProject.Application.Shared.Contracts.Mediator.Interfaces;
using FluentValidation;
using MediatR;

namespace ExampleProject.Application.Shared.Contracts.Validator;

public abstract class BaseValidator<T> : AbstractValidator<T> where T : IBaseRequest
{
    
}