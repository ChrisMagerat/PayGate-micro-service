using Microsoft.AspNetCore.Mvc;
using PayGateMicroService.API.Controllers.Shared;
using PayGateMicroService.Application.PayGate.Commands;

namespace PayGateMicroService.API.Controllers.PayGate;

public class PayGateController : ApiBaseController
{
    [HttpPost("pay-gate-pay")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    public async Task<ActionResult> InitialPay([FromQuery] PayGateCommand payGateCommand, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(payGateCommand, cancellationToken));
    }
}