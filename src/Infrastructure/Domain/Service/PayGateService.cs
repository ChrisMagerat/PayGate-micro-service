using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Logging;
using PayGateMicroService.Domain.Services.PayGateService;
using PayGateMicroService.Domain.Services.PayGateService.Request;
using PayGateMicroService.Domain.Services.PayGateService.Response;
using PayGateMicroService.Infrastructure.Configuration;

namespace PayGateMicroService.Infrastructure.Domain.Service;

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

    public async Task<PayGateResponse?> RequestPaymentAsync(PayGateRequest request, CancellationToken cancellationToken)
    {
        var checksum = CalculateChecksum(request);
        var response = await _businessClient
            .Request()
            .PostJsonAsync(checksum, cancellationToken: cancellationToken);
        var initialResponse = await response.ResponseMessage.Content.ReadFromJsonAsync<PayGateResponse>(cancellationToken: cancellationToken);
        return initialResponse;
    }
    
    private string CalculateChecksum(PayGateRequest request)
    {
        var dataString = "";
        if (request.NotifyUrl  == null)
        {
            dataString =
                $"{request.PayGateId}{request.Reference}{request.Amount}{request.Currency}{request.ReturnUrl}{request.TransactionDate}{request.Locale}{request.Country}{request.Email}{_payGateConfiguration.PayGateKey}";
        }
        else
        {
            dataString =
                $"{request.PayGateId}{request.Reference}{request.Amount}{request.Currency}{request.ReturnUrl}{request.TransactionDate}{request.Locale}{request.Country}{request.Email}{request.NotifyUrl}{_payGateConfiguration.PayGateKey}";
        }

        using MD5 md5 = MD5.Create();
        byte[] data = Encoding.UTF8.GetBytes(dataString);
        byte[] hash = md5.ComputeHash(data);
            
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            builder.Append(hash[i].ToString("x2"));
        }

        return builder.ToString();
    }
}