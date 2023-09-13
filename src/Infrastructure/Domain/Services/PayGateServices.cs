using ExampleProject.Domain.Services.PayGateService;
using ExampleProject.Infrastructure.Configuration;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Logging;

namespace ExampleProject.Infrastructure.Domain.Services;

public class PayGateServices : IPayGateService
{
    private readonly ILogger _logger;
    private readonly IFlurlClient _businessClient;
    private readonly PayGateConfiguration _payGateConfiguration;

    public PayGateServices(ILogger logger, IFlurlClientFactory businessClient, PayGateConfiguration payGateConfiguration)
    {
        _logger = logger;
        _businessClient = businessClient.Get(payGateConfiguration.BusinessUrl);
        _payGateConfiguration = payGateConfiguration;
    }
}