using Microsoft.AspNetCore.Mvc;
using PayGate.Application.ExampleDomain.Requests;

namespace PayGate.API.Controllers.ExampleDomain.Contracts;

public class AddExampleEntityContract
{
    public AddExampleEntityContract()
    {
    }

    public AddExampleEntityContract(AddExampleEntityRequest request)
    {
        AddBody = request;
    }

    [FromBody] 
    public AddExampleEntityRequest AddBody { get; set; } = null!;
}