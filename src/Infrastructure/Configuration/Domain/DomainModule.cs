using Autofac;
using PayGate.Domain.Services.PayGateService;
using PayGate.Infrastructure.Configuration.Quartz;
using PayGate.Infrastructure.Domain.Services;
using PayGate.Infrastructure.Persistence;
using Module = Autofac.Module;

namespace PayGate.Infrastructure.Configuration.Domain;

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