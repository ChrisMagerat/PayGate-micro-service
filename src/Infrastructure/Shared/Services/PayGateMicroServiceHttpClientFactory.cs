using System.Net;
using Flurl.Http.Configuration;
using Polly;
using Polly.Contrib.WaitAndRetry;

namespace PayGateMicroService.Infrastructure.Shared.Services;

public class RetryHandler : DelegatingHandler
{
    private readonly IAsyncPolicy<HttpResponseMessage> _policy = Policy<HttpResponseMessage>
        .Handle<HttpRequestException>()
        .OrResult(r => r.StatusCode is
            HttpStatusCode.InternalServerError or
            HttpStatusCode.GatewayTimeout or
            HttpStatusCode.BadGateway or
            HttpStatusCode.RequestTimeout)
        .WaitAndRetryAsync(
            Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 3)
        );
    
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        return _policy.ExecuteAsync(ct => base.SendAsync(request, ct), cancellationToken);
    }
}


public class PayGateMicroServiceHttpClientFactory: DefaultHttpClientFactory
{
    public override HttpMessageHandler CreateMessageHandler()
    {
        return new RetryHandler()
        {
            InnerHandler = base.CreateMessageHandler()
        };
    }
}