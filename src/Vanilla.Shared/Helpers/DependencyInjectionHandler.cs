using Microsoft.Extensions.DependencyInjection;
using MySaviors.Helpers.Extensions;

namespace Vanilla.Shared.Helpers;

public static class DependencyInjectionHandler
{
    private static IServiceCollection _services;

    private static ServiceProvider _provider;

    public static IServiceCollection AddDIHandler(this IServiceCollection services)
    {
        if (_services == null)
        {
            _services = services;
        }

        if (_provider == null)
        {
            _provider = _services.BuildServiceProvider();
        }

        return _services;
    }

    public static TInstance GetInstance<TInstance>()
    {
        return (TInstance)(_provider.IsNull() ? ((object)default(TInstance)) : ((object)ServiceProviderServiceExtensions.GetService<TInstance>(_provider)));
    }

    public static IEnumerable<TInstance> GetInstances<TInstance>()
    {
        return _provider.IsNull() ? null : ServiceProviderServiceExtensions.GetServices<TInstance>(_provider);
    }

    public static void Dispose()
    {
        if (_provider.IsNotNull())
        {
            _provider.Dispose();
            _provider = null;
        }

        if (_services.IsNotNull())
        {
            _services.Clear();
            _services = null;
        }
    }
}
