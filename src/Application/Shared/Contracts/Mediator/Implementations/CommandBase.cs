using System.Text.Json.Serialization;
using ExampleProject.Application.Shared.Contracts.Mediator.Interfaces;
using NSwag.Annotations;

namespace ExampleProject.Application.Shared.Contracts.Mediator.Implementations;

public abstract class CommandBase : ICommand, ICommandBase
{
    [OpenApiIgnore]
    [JsonIgnore]
    public Guid Id
    { get; }

    protected CommandBase()
    {
        Id = Guid.NewGuid();
    }

    protected CommandBase(Guid id)
    {
        Id = id;
    }
}

public abstract class CommandBase<TResult> : ICommandWithResult<TResult>, ICommandBase
{
    [OpenApiIgnore]
    [JsonIgnore]
    public Guid Id { get; }

    protected CommandBase()
    {
        Id = Guid.NewGuid();
    }

    protected CommandBase(Guid id)
    {
        Id = id;
    }
}