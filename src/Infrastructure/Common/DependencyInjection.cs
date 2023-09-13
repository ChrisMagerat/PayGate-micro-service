using System.Reflection;
using Azure.Storage.Blobs;
using ExampleProject.Application.Common.Interfaces;
using ExampleProject.Application.Identity.Configuration;
using ExampleProject.Domain.Common;
using ExampleProject.Domain.Identity.IdentityUser;
using ExampleProject.Domain.Shared.Configuration;
using ExampleProject.Infrastructure.Configuration;
using ExampleProject.Infrastructure.Configuration.Common;
using ExampleProject.Infrastructure.Persistence;
using ExampleProject.Infrastructure.Shared.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExampleProject.Infrastructure.Common;

public static class DependencyInjection
{
    public static void AddInfrastructure(this WebApplicationBuilder builder, AppSettings appSettings)
    {
        builder.AddIdentity();
        builder.AddIdentityServer();
        builder.Services.AddDomainEvents();
        builder.Services.AddDatabase(appSettings.ConnectionString);
        builder.AddBlobStorage();
        builder.Services.AddRepositories();
        builder.Services.AddServices();
        builder.Services.AddScoped<IUnitOfWork, EntityFrameworkUnitOfWork>();

    }

    private static void AddDatabase(this IServiceCollection serviceCollection, string? connectionString)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString, 
                optionsBuilder => 
                {
                    optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    optionsBuilder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                }));
       
        //It has to do with how we handle DateTimes.
        //They have a type  Unspecified, Local, or UTC. 
        //Cases where the type is unspecified EF gets confused
        //This tells ef to treat those cases as Local
        //If we specify the type on create this should never be a problem
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    
    private static void AddRepositories(this IServiceCollection services)
    {
        var infrastructureAssembly = typeof(Repository).Assembly;
        var domainAssembly = typeof(IRepository).Assembly;
        RepositoryAndServicesHelper(services,"Repository", domainAssembly, infrastructureAssembly);
    }

    

    private static void AddServices(this IServiceCollection services)
    {
        var infrastructureAssembly = typeof(Repository).Assembly;
        var applicationAssembly = typeof(ICurrentUserService).Assembly;
        RepositoryAndServicesHelper(services,"Service", applicationAssembly, infrastructureAssembly);
    }

    private static void RepositoryAndServicesHelper(this IServiceCollection services
        , string suffix
        , Assembly interfaceAssembly
        , Assembly implementationAssembly)
    {
        var serviceTypes = interfaceAssembly.GetTypes()
            .Where(t => t.Name.EndsWith(suffix) && t.IsInterface);
        
        foreach (var serviceType in serviceTypes)
        {
            var implementationType = implementationAssembly.GetTypes().FirstOrDefault(t =>
                t is { IsClass: true, IsAbstract: false } && serviceType.IsAssignableFrom(t));

            if (implementationType != null)
            {
                services.AddScoped(serviceType, implementationType);
            }
        }
    }

    private static void AddIdentity(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication();
        builder.Services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;

                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 4;
            })
            .AddSignInManager<SignInManager<User>>()
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }

    private static void AddIdentityServer(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration.GetSection("BaseUrls").Get<BaseUrlsConfiguration>();
        builder.Services.AddSingleton(new HostUrl(configuration.HostUrl));
        builder.Services.AddSingleton(new EmailVerificationUrl(configuration.EmailConfirmationUrl));
        builder.Services.AddSingleton(new PasswordResetUrl(configuration.PasswordResetUrl));
        
        var authenticationConfiguration =
            builder.Configuration.GetSection("Authentication").Get<AuthenticationConfiguration>();
        var identityServerBuilder = builder.Services.AddIdentityServer()
            .AddInMemoryIdentityResources(IdentityServerConfiguration.IdentityResources)
            .AddInMemoryApiScopes(IdentityServerConfiguration.ApiScopes)
            .AddInMemoryApiResources(IdentityServerConfiguration.GetApiResources(authenticationConfiguration))
            .AddInMemoryClients(IdentityServerConfiguration.GetClients(authenticationConfiguration))
            .AddOperationalStore<ApplicationDbContext>(options =>
            {
                options.EnableTokenCleanup = true;
                options.ConfigureDbContext = dbBuilder => dbBuilder.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                options.PersistedGrants.Schema = "identity";
                options.DeviceFlowCodes.Schema = "identity";
                options.TokenCleanupInterval = 3600;
            })
            .AddAspNetIdentity<User>();
    
        if (builder.Environment.IsDevelopment())
        {
            identityServerBuilder.AddDeveloperSigningCredential();
        }
    
        builder.Services.AddAuthentication("Bearer")
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = authenticationConfiguration.Authority;
                options.RequireHttpsMetadata = false;
                options.ApiName = authenticationConfiguration.Audience;
            });
    
        builder.Services.AddAuthorization();
    }

    private static void AddDomainEvents(this IServiceCollection services)
    {
        services.AddSingleton<IDomainEventPublisher, DomainEventPublisher>();
    }

    private static void AddBlobStorage(this WebApplicationBuilder builder)
    {
        var azureConfiguration = builder.Configuration.GetSection("Azure").Get<AzureConfiguration>();
        builder.Services.AddSingleton(azureConfiguration);
        builder.Services.AddSingleton(_ => new BlobServiceClient(azureConfiguration.BlobStorageConnectionString));
    }
}
