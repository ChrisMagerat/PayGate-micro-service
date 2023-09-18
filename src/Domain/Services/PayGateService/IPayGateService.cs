using PayGateMicroService.Domain.Services.PayGateService.Request;
using PayGateMicroService.Domain.Services.PayGateService.Response;

namespace PayGateMicroService.Domain.Services.PayGateService;

public interface IPayGateService
{
    public Task<PayGateResponse?> RequestPaymentAsync(PayGateRequest request, CancellationToken cancellationToken);
}