using Vanilla.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Vanilla.Infra.Data.Configurations;

public class DisposableScopedContextWrapper : IDisposable
{
    private readonly IServiceScope _scope;
    public DbContext Context { get; }

    public DisposableScopedContextWrapper(IServiceScope scope)
    {
        _scope = scope;
        Context = _scope.ServiceProvider.GetService<VanillaContext>();
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}
