using PayGateMicroService.Infrastructure.Configuration;
using NSwag.AspNetCore;

namespace PayGateMicroService.API.Configuration;

public static class WebAppConfiguration
{
    public static void HttpRequestPipeline(this WebApplication app, AuthenticationConfiguration authenticationConfiguration)
    {
        if (!app.Environment.IsDevelopment()) return;
        app.UseOpenApi();
        app.UseSwaggerUi3(options =>
        {
            if (app.Environment.IsDevelopment())
            {
                options.OAuth2Client = new OAuth2ClientSettings
                {
                    ClientId = authenticationConfiguration.OAuth2.ClientId,
                    ClientSecret = authenticationConfiguration.OAuth2.Secret,
                };
            }
        });
    }
}