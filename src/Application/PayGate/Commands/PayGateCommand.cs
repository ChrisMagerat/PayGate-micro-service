using Microsoft.Extensions.Logging;
using PayGateMicroService.Application.Shared.Contracts.Mediator;
using PayGateMicroService.Application.Shared.Contracts.Mediator.Implementations;
using PayGateMicroService.Domain.Services.PayGateService;
using PayGateMicroService.Domain.Services.PayGateService.Request;
using PayGateMicroService.Domain.Services.PayGateService.Response;

namespace PayGateMicroService.Application.PayGate.Commands;

public class PayGateCommand : CommandBase<PayGateResponse>
{
    public string PayGateId { get; set; }
    public string Reference { get; set; }
    public int Amount { get; set; }
    public string ReturnUrl { get; set; }
    public string Currency { get; set; }
    public string Locale { get; set; }
    public string Country { get; set; }
    public string Email { get; set; }
}

public class PayGateCommandHandler : BaseAsyncRequestHandler<PayGateCommand, PayGateResponse>
{
    private readonly IPayGateService _payGateService;

    public PayGateCommandHandler(ILogger<PayGateCommandHandler> logger, IPayGateService payGateService) : base(logger)
    {
        _payGateService = payGateService;
    }

    public override async Task<PayGateResponse> Handle(PayGateCommand request, CancellationToken cancellationToken)
    {
        var initialRequest = new PayGateRequest(request.PayGateId, request.Reference, request.Amount, request.Currency, request.ReturnUrl,
            DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), request.Locale, request.Country, request.Email);
        var initialResponse = await _payGateService.RequestPaymentAsync(initialRequest, cancellationToken);
        return initialResponse;
    }
}