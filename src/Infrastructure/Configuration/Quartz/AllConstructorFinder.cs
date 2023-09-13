using System.Collections.Concurrent;
using System.Reflection;
using Autofac.Core.Activators.Reflection;
using SendGrid.Helpers.Errors.Model;

namespace PayGate.Infrastructure.Configuration.Quartz;

public class AllConstructorFinder : IConstructorFinder
{
    private static readonly ConcurrentDictionary<Type, ConstructorInfo[]> Cache =
        new ConcurrentDictionary<Type, ConstructorInfo[]>();

    public ConstructorInfo[] FindConstructors(Type targetType)
    {
        var result = Cache.GetOrAdd(
            targetType,
            t => t.GetTypeInfo().DeclaredConstructors.ToArray());

        return result.Length > 0 ? result : throw new NotFoundException(targetType.ToString());
    }
}