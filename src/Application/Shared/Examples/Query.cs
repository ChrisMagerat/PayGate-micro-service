using FluentValidation;
using Microsoft.Extensions.Logging;
using PayGate.Application.Shared.Contracts.Mediator;
using PayGate.Application.Shared.Contracts.Mediator.Implementations;
using PayGate.Application.Shared.Contracts.Validator;

namespace PayGate.Application.Shared.Examples;

public class Query: QueryBase<QueryResult>
{
    public Query(string data)
    {
        Data = data;
    }

    public string Data { get; }
}

public class Validator : BaseValidator<Query>
{
    public Validator()
    {
        RuleFor(x => x.Data)
            .NotEmpty().WithMessage("Data must not be empty.")
            .MinimumLength(3).WithMessage("Data must be at least 3 characters long.");
    }
}
public class QueryHandler : BaseAsyncRequestHandler<Query, QueryResult>
{
    public QueryHandler(ILogger<BaseAsyncRequestHandler<Query, QueryResult>> logger) : base(logger)
    {
    }

    public async override Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
    {
        return new QueryResult(request.Data);
    }
}


public class QueryResult
{
    public QueryResult(string data)
    {
        Data = data;
    }

    public string Data { get; }
}