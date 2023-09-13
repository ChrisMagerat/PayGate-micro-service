using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PayGate.Application.Shared.Contracts.Behaviors;
using PayGate.Application.Shared.Contracts.Mediator;
using PayGate.Application.Shared.Contracts.Validator;

namespace PayGate.Application.Common;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR();
        services.AddValidation();
    }

    private static void AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(BaseAsyncRequestHandler<>).Assembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }

    private static void AddValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(BaseValidator<>).Assembly);

        var assembly = typeof(BaseValidator<>).Assembly;
        var validatorTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseValidator<>)))
            .ToList();

        foreach (var validatorType in validatorTypes)
        {
            var validatorInterface = validatorType.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>));
            if (validatorInterface != null)
            {
                services.AddTransient(validatorInterface, validatorType);
            }
        }
    }
}
