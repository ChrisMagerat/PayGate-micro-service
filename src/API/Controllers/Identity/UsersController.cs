using PayGateMicroService.API.Controllers.Shared;
using PayGateMicroService.Application.Identity.Queries;
using PayGateMicroService.Domain.Identity.IdentityUser;
using Microsoft.AspNetCore.Mvc;

namespace PayGateMicroService.API.Controllers.Identity;

public class UsersController : ApiBaseController
{
    [HttpGet("current")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    public async Task<ActionResult<User>> GetCurrentUser(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new CurrentUserQuery(), cancellationToken));
    }
}