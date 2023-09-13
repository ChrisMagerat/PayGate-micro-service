using ExampleProject.API.Controllers.Identity.Contracts;
using ExampleProject.API.Controllers.Shared;
using ExampleProject.Application.Identity.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExampleProject.API.Controllers.Identity;

public class AccountsController : ApiBaseController
{
    [AllowAnonymous]
    [HttpPost("sign-up")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Create([FromBody] SignUpUserContract contract, CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new SignUpUserCommand(contract.FirstName, contract.LastName, contract.Email, contract.Password), cancellationToken);

        return NoContent();
    }
    
    [AllowAnonymous]
    [HttpPost("verify-email")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    public async Task<ActionResult> VerifyEmailAsync([FromBody] VerifyEmailContract contract, CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new VerifyEmailCommand(contract.Email, contract.Token), cancellationToken);
        
        return NoContent();
    }
    
    [AllowAnonymous]
    [HttpPost("forgot-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> ForgotPasswordAsync(ForgotPasswordContract contract,
        CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new ForgotPasswordCommand(contract.Email), cancellationToken);
        
        return NoContent();
    }

    [AllowAnonymous]
    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ResetPasswordAsync([FromBody] ResetPasswordContract contract,
        CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new ResetPasswordCommand(contract.Email, contract.Token, contract.Password), cancellationToken);
        
        return NoContent();
    }
}