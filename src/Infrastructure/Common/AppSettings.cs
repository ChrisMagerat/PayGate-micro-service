using PayGate.Infrastructure.Configuration;

namespace PayGate.Infrastructure.Common;

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