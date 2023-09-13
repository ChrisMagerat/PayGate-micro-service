using System.Collections.Specialized;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Serilog;

namespace ExampleProject.Infrastructure.Configuration.Quartz;

public class QuartzStartup
{
    private static IScheduler _scheduler;

    internal static void Initialize(ILogger logger)
    {
        logger.Information("Quartz starting...");

        var schedulerConfiguration = new NameValueCollection();
        schedulerConfiguration.Add("quartz.scheduler.instanceName", "Tipsi");

        ISchedulerFactory schedulerFactory = new StdSchedulerFactory(schedulerConfiguration);
        _scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

        LogProvider.SetCurrentLogProvider(new SerilogLogProvider(logger));

        _scheduler.Start().GetAwaiter().GetResult();

        logger.Information("Quartz started.");
    }

    internal static void StopQuartz()
    {
        _scheduler?.Shutdown();
    }
}