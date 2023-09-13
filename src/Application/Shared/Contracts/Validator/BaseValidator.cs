using FluentValidation;
using MediatR;

namespace PayGate.Application.Shared.Contracts.Validator;

public abstract class BaseValidator<T> : AbstractValidator<T> where T : IBaseRequest
{
    
}