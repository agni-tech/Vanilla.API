using Vanilla.Shared.Interfaces;

namespace Vanilla.Shared.Helpers;

public static class ServiceLocator
{
    public static IContainer Container { get; set; }

    public static void Initialize(IContainer container)
        => Container = container;
}