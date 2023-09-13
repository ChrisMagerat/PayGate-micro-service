using Microsoft.AspNetCore.Mvc;
using PayGate.Application.ExampleDomain.Requests;

namespace PayGate.API.Controllers.ExampleDomain.Contracts;

public class UpdateExampleEntityContract
{
    public UpdateExampleEntityContract()
    {
    }

    public UpdateExampleEntityContract(UpdateExampleEntityRequest request)
    {
        UpdateBody = request;
    }

    [FromBody] public UpdateExampleEntityRequest UpdateBody { get; set; } = null!;
}