using System.Security.Cryptography;
using Autofac;
using Azure.Storage.Blobs;
using ExampleProject.Infrastructure.Common;
using ExampleProject.Infrastructure.Configuration.Domain;
using ExampleProject.Infrastructure.Configuration.Logging;
using ExampleProject.Infrastructure.Configuration.Quartz;
using Flurl.Http.Configuration;
using Serilog;
using Serilog.AspNetCore;

namespace ExampleProject.Infrastructure.Configuration;

public class ApplicationStartup
{
    private static IContainer _container;

    public static void Initialize(
        string connectionString,
        ILogger logger,
        PayGateConfiguration payGateConfiguration,
        AzureConfiguration azureConfiguration
    )
    {
        var moduleLogger = logger.ForContext("Module", "Application");

        ConfigureCompositionRoot(
            connectionString,
            payGateConfiguration,
            azureConfiguration,
            logger);

        QuartzStartup.Initialize(moduleLogger);

        // EventsBusStartup.Initialize(moduleLogger);
    }

    public static void Stop()
    {
        QuartzStartup.StopQuartz();
    }

    private static void ConfigureCompositionRoot(
        string connectionString,
        PayGateConfiguration payGateConfiguration,
        AzureConfiguration azureConfiguration,
        ILogger initialLogger)
    {
        var containerBuilder = new ContainerBuilder();

        var loggingModule = new LoggingModule(initialLogger.ForContext("Module", "Application"));
        containerBuilder.RegisterModule(loggingModule);

        var loggerFactory = new SerilogLoggerFactory(initialLogger);
        containerBuilder.RegisterModule(new DomainModule());
        
        containerBuilder.RegisterType(typeof(DefaultFlurlClientFactory))
                    .As(typeof(IFlurlClientFactory))
                    .SingleInstance();

        containerBuilder.RegisterInstance(typeof(PayGateConfiguration));
        containerBuilder.RegisterInstance(azureConfiguration);
        
        var azureBlobClient = new BlobServiceClient(azureConfiguration.BlobStorageConnectionString);
        containerBuilder.RegisterInstance(azureBlobClient);
        var rsaCryptoServiceProvider = new RSACryptoServiceProvider();
        containerBuilder.RegisterInstance(rsaCryptoServiceProvider);
        _container = containerBuilder.Build();

        ApplicationCompositionRoot.SetContainer(_container);
    }
}