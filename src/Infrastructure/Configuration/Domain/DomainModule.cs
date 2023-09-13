using System.Reflection;
using Autofac;
using ExampleProject.Domain.Services.PayGateService;
using ExampleProject.Infrastructure.Configuration.Quartz;
using ExampleProject.Infrastructure.Domain.Services;
using ExampleProject.Infrastructure.Persistence;
using Module = Autofac.Module;

namespace ExampleProject.Infrastructure.Configuration.Domain;

internal class DomainModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        
        var infrastructureAssembly = typeof(ApplicationDbContext).Assembly;

        builder.RegisterAssemblyTypes(infrastructureAssembly)
            .Where(type => type.Name.EndsWith("Service"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope()
            .FindConstructorsWith(new AllConstructorFinder());
        
        builder.RegisterType<PayGateServices>()
            .As<IPayGateService>()
            .SingleInstance();
    }
}