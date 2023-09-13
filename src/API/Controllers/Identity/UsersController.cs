using Microsoft.AspNetCore.Mvc;
using PayGate.API.Controllers.Shared;
using PayGate.Application.Identity.Queries;
using PayGate.Domain.Identity.IdentityUser;

namespace PayGate.API.Controllers.Identity;

public class UsersController : ApiBaseController
{
    [HttpGet("current")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    public async Task<ActionResult<User>> GetCurrentUser(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new CurrentUserQuery(), cancellationToken));
    }
}