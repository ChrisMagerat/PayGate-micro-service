using Microsoft.AspNetCore.Mvc;

namespace PayGateMicroService.API.Controllers.Identity.Contracts;

public class ResetPasswordContract
{
    [FromBody] public string Email { get; set; }
    [FromBody] public string Token { get; set; }
    [FromBody] public string Password { get; set; }
}