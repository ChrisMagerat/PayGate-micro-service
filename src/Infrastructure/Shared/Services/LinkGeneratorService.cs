using PayGateMicroService.Application.Shared.Services;
using Flurl;

namespace PayGateMicroService.Infrastructure.Shared.Services;

public class LinkGeneratorService: ILinkGeneratorService
{
    public string GenerateLink(string host, string route, object query)
    {
        var encodedUrl = host.AppendPathSegment(route).SetQueryParams(query).ToUri().ToString();
        return encodedUrl ?? String.Empty;
    }
}