using Microsoft.AspNetCore.Mvc;

namespace PayGate.API.Controllers.Identity.Contracts;

public class SignUpUserContract
{
    [FromBody] public string FirstName { get; set; }
    [FromBody] public string LastName { get; set; }
    [FromBody] public string Email { get; set; }
    [FromBody] public string Password { get; set; }
}