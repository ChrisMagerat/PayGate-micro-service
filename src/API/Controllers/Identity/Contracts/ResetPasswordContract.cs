using Microsoft.AspNetCore.Mvc;

namespace ExampleProject.API.Controllers.Identity.Contracts;

public class ResetPasswordContract
{
    [FromBody] public string Email { get; set; }
    [FromBody] public string Token { get; set; }
    [FromBody] public string Password { get; set; }
}