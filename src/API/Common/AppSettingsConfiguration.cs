using PayGateMicroService.Infrastructure.Common;
using PayGateMicroService.Infrastructure.Configuration;
using SendGrid.Helpers.Errors.Model;

namespace PayGateMicroService.API.Common;

public static class AppSettingsConfiguration
{
    public static AppSettings GetAppSettings(this WebApplicationBuilder builder)
    {
        var authenticationConfiguration = builder.Configuration.GetSection("Authentication")
            .Get<AuthenticationConfiguration>()
            ?? throw new NotFoundException("We could not find your authentication configuration");
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                               ?? throw new NotFoundException("We could not find a sql connection string");
        
        return new AppSettings(
            authenticationConfiguration, connectionString);
    }
}