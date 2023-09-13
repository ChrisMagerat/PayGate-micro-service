using ExampleProject.Application.Shared.Contracts.Mediator.Interfaces;
using NSwag.Annotations;

namespace ExampleProject.Application.Shared.Contracts.Mediator.Implementations;

public class QueryBase<TResult> : IQuery<TResult>
{
    [OpenApiIgnore] 
    public Guid Id { get; }

    protected QueryBase()
    {
        Id = Guid.NewGuid();
    }

    protected QueryBase(Guid id)
    {
        Id = id;
    }
}