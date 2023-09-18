using PayGateMicroService.Application.Common.Exceptions;
using PayGateMicroService.Domain.Common.Rules;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SendGrid.Helpers.Errors.Model;

namespace PayGateMicroService.API.Filters;

public class ApiExceptionFilterAttribute: ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilterAttribute()
    {
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            {typeof(InvalidOperationException), HandleInvalidOperationException},
            {typeof(ValidationException), HandleValidationException},
            {typeof(NotFoundException), HandleNotFoundException},
            {typeof(BadHttpRequestException), HandleBadRequestException},
            {typeof(BusinessRuleValidationException), HandleBusinessRuleValidationException},
            {typeof(ArgumentException), HandleArgumentException},
            {typeof(ArgumentNullException), HandleArgumentNullException},
            {typeof(BadRequestException), HandleBadRequestException},
            {typeof(IdentityException), HandleIdentityException}
        };
    }
    
    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        var type = context.Exception.GetType();
        if (_exceptionHandlers.TryGetValue(type, out var handler))
        {
            handler.Invoke(context);
        }
    }
    
    private void ConstructObjectResult(ExceptionContext context, ProblemDetails details)
    {
        context.Result = new ObjectResult(details)
        {
            StatusCode = details.Status
        };

        context.ExceptionHandled = true;
    }


    private void HandleInvalidOperationException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status422UnprocessableEntity,
            Title = "Invalid operation attempted",
            Type = "https://httpstatuses.com/422",
            Detail = context.Exception.Message
        };

        ConstructObjectResult(context, details);
    }

    private void HandleValidationException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Data Validation Failed",
            Type = "https://httpstatuses.com/400",
            Detail = context.Exception.Message
        };

        ConstructObjectResult(context, details);

    }
    
    
    private void HandleNotFoundException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Not Found",
            Type = "https://httpstatuses.com/404",
            Detail = context.Exception.Message
        };

        ConstructObjectResult(context, details);

    }

    private void HandleBadRequestException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Bad Request",
            Type = "https://httpstatuses.com/400",
            Detail = context.Exception.Message
        };

        ConstructObjectResult(context, details);

    }
    
    private void HandleBusinessRuleValidationException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Business Rule Validation Exception",
            Type = "https://httpstatuses.com/400",
            Detail = context.Exception.Message
        };

        ConstructObjectResult(context, details);

    }
    
    private void HandleArgumentNullException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Argument Null Exception",
            Type = "https://httpstatuses.com/400",
            Detail = context.Exception.Message
        };

        ConstructObjectResult(context, details);

    }

    private void HandleArgumentException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Argument Exception",
            Type = "https://httpstatuses.com/400",
            Detail = context.Exception.Message
        };

        ConstructObjectResult(context, details);

    }
    private void HandleIdentityException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Identity Exception",
            Type = "https://httpstatuses.com/400",
            Detail = context.Exception.Message
        };

        ConstructObjectResult(context, details);

    }
}