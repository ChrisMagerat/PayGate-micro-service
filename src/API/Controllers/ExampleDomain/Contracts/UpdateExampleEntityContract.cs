using PayGateMicroService.Application.ExampleDomain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace PayGateMicroService.API.Controllers.ExampleDomain.Contracts;

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