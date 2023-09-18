namespace PayGateMicroService.Application.Identity.Configuration;

public class HostUrl
{
    public string BaseLink { get; set; }

    public HostUrl(string baseLink)
    {
        BaseLink = baseLink;
    }
}