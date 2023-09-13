using Microsoft.AspNetCore.Mvc;

namespace PayGate.API.Controllers.Identity.Contracts;

public class ForgotPasswordContract
{
    [FromBody] public string Email { get; set; }
}