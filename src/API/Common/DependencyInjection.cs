using ExampleProject.API.Contracts;
using ExampleProject.API.Configuration;
using Azure.Storage.Blobs;
using ExampleProject.API.Filters;
using ExampleProject.Application.Common.Interfaces;
using ExampleProject.Application.Shared.Services;
using ExampleProject.Infrastructure.Common;
using ExampleProject.Infrastructure.Configuration;
using ExampleProject.Infrastructure.Shared.Services;
using Flurl.Http;
using Flurl.Http.Configuration;
using NSwag;
using Serilog;
using ExampleProject.Infrastructure.Shared.Configuration;

namespace ExampleProject.API.Common;

public static class DependencyInjection
{
    public static void AddApi(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddCurrentUserServices();
        builder.AddSwaggerDocument();
        builder.AddSerilog();
        builder.Services.AddSwaggerDocument();
    }

    private static void AddControllers(this IServiceCollection services)
    {
        services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>())
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressInferBindingSourcesForParameters = true;
            });
        
        services.AddEndpointsApiExplorer();
    }

    private static void AddCurrentUserServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
    }

    private static void AddSwaggerDocument(this WebApplicationBuilder builder)
    {
        var UrlConfiguration = builder.Configuration.GetSection("UrlConfiguration").Get<UrlConfiguration>();
        builder.Services.AddSwaggerDocument(options =>
        {
            options.Description = "Codehesion API";
            options.DocumentName = "Codehesion";
            options.Title = "Api-Gateway";
            options.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Password = new OpenApiOAuthFlow
                    {
                        RefreshUrl = UrlConfiguration.RefreshUrl,
                        TokenUrl = UrlConfiguration.TokenUrl,
                        Scopes = new Dictionary<string, string>
                        {
                            {Scopes.Email, nameof(Scopes.Email)},
                            {Scopes.OpenId, nameof(Scopes.OpenId)},
                            {Scopes.AdminPortal, nameof(Scopes.AdminPortal)},
                            {Scopes.Profile, nameof(Scopes.Profile)},
                            {Scopes.Role, nameof(Scopes.Role)},
                            {Scopes.OfflineAccess, nameof(Scopes.OfflineAccess)}
                        },
                    }
                }
            });
        });
    }

    private static void AddSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.Enrich.FromLogContext()
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentName()
                .WriteTo.Console()
                .WriteTo.Seq(context.Configuration.GetConnectionString("Seq") ?? throw new Exception("Seq connection string missing"));
        });
    }

    public static void Add3rdPartyIntegrations(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddSingleton<IFlurlClientFactory, DefaultFlurlClientFactory>();
        FlurlHttp.Configure(settings => settings.HttpClientFactory = new ExampleProjectHttpClientFactory());
        services.AddTransient<ILinkGeneratorService, LinkGeneratorService>();
    }
    
    
    private static async Task ConfigureBlobStorage(this WebApplicationBuilder builder)
    {
        var azureConfiguration = builder.Configuration.GetSection("Azure").Get<AzureConfiguration>();
        var initializer = new BlobContainerInitializer(azureConfiguration.BlobStorageConnectionString);
        builder.Services.AddSingleton(_ => new BlobServiceClient(azureConfiguration.BlobStorageConnectionString));
        await initializer.InitializeContainerAsync(CancellationToken.None);
    }
}
