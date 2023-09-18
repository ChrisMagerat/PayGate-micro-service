using Microsoft.AspNetCore.Mvc;

namespace PayGateMicroService.API.Controllers.Identity.Contracts;

public class ForgotPasswordContract
{
    [FromBody] public string Email { get; set; }
}