using ExampleProject.API.Controllers.Shared;
using ExampleProject.Application.Identity.Queries;
using ExampleProject.Domain.Identity.IdentityUser;
using Microsoft.AspNetCore.Mvc;

namespace ExampleProject.API.Controllers.Identity;

public class UsersController : ApiBaseController
{
    [HttpGet("current")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    public async Task<ActionResult<User>> GetCurrentUser(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new CurrentUserQuery(), cancellationToken));
    }
}