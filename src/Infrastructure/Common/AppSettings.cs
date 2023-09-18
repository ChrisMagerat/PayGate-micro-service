using PayGateMicroService.Infrastructure.Configuration;

namespace PayGateMicroService.Infrastructure.Common;

public class AppSettings
{
    public AppSettings(AuthenticationConfiguration authenticationConfiguration, string connectionString)
    {
        AuthenticationConfiguration = authenticationConfiguration;
        ConnectionString = connectionString;
    }

    public AuthenticationConfiguration AuthenticationConfiguration { get; }
    public string ConnectionString { get; }

}