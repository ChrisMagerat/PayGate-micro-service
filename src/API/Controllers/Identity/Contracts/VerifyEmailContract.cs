using Microsoft.AspNetCore.Mvc;

namespace PayGateMicroService.API.Controllers.Identity.Contracts;

public class VerifyEmailContract
{
    [FromBody] public string Email { get; set; }
    // TODO: mask token from logging
    [FromBody] public string Token { get; set; }
}