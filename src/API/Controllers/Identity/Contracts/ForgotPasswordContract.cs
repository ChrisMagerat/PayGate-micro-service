using Microsoft.AspNetCore.Mvc;

namespace ExampleProject.API.Controllers.Identity.Contracts;

public class ForgotPasswordContract
{
    [FromBody] public string Email { get; set; }
}