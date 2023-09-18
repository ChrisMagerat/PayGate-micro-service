using PayGateMicroService.Infrastructure.Persistence;
using PayGateMicroService.Infrastructure.Shared.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PayGateMicroService.Infrastructure.Common;

public static class Middleware
{
    public static async Task Migrate(this WebApplication app, string[] args)
    {
        var environment = app.Services.GetRequiredService<IHostEnvironment>();
        if (!environment.IsDevelopment() && !args.Contains("Migrate"))
        {
            return;
        }

        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
       
        var applicationDbContext = services.GetRequiredService<ApplicationDbContext>();
        if (applicationDbContext.Database.IsNpgsql())
        {
            await applicationDbContext.Database.MigrateAsync();
        }
            
    }

    public static async Task UseBlobStorage(this WebApplication app)
    {
        var azureConfiguration = app.Services.GetRequiredService<AzureConfiguration>();
        var initializer = new BlobContainerInitializer(azureConfiguration.BlobStorageConnectionString);
        await initializer.InitializeContainerAsync(CancellationToken.None);
    }
}
