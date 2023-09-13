using ExampleProject.Application.ExampleDomain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ExampleProject.API.Controllers.ExampleDomain.Contracts;

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