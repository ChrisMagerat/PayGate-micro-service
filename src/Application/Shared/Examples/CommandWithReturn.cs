using ExampleProject.Application.Shared.Contracts.Mediator;
using ExampleProject.Application.Shared.Contracts.Mediator.Implementations;
using ExampleProject.Application.Shared.Contracts.Mediator.Interfaces;
using ExampleProject.Application.Shared.Contracts.Validator;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ExampleProject.Application.Shared.Examples;

public class CommandWithReturn: CommandBase<CommandWithReturnResult>
{
    public CommandWithReturn(string data)
    {
        Data = data;
    }

    public string Data {
        get;
    }
}

public class CommandWithReturnValidator : BaseValidator<CommandWithReturn>
{
    public CommandWithReturnValidator()
    {
        RuleFor(x => x.Data)
            .NotEmpty().WithMessage("Data must not be empty.")
            .MinimumLength(3).WithMessage("Data must be at least 3 characters long.");
    }
}

public class CommandWithReturnHandler : BaseAsyncRequestHandler<CommandWithReturn, CommandWithReturnResult>
{
    public CommandWithReturnHandler(ILogger<BaseAsyncRequestHandler<CommandWithReturn, CommandWithReturnResult>> logger) : base(logger)
    {
    }

    public async override Task<CommandWithReturnResult> Handle(CommandWithReturn request, CancellationToken cancellationToken)
    {
        return new CommandWithReturnResult(request.Data);
    }
}

public class CommandWithReturnResult
{
    public CommandWithReturnResult(string result)
    {
        Result = result;
    }

    public string Result { get; }
}