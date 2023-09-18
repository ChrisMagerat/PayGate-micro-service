namespace PayGateMicroService.Application.Shared.Services;

public interface ILinkGeneratorService
{
    string GenerateLink(string host, string route, object query);
}