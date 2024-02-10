using Vanilla.Shared.Interfaces;

namespace Vanilla.Shared.Helpers;

public static class NotificationsWrapper
{
    private static INotifications GetContainer()
        => ServiceLocator.Container.GetService<INotifications>(typeof(INotifications));

    public static void AddMessage(string message)
    {
        GetContainer().AddMessage(message);
    }

    public static void AddMessage(List<string> message)
        => message.ForEach(msg => GetContainer().AddMessage(msg));

    public static bool IsValid()
        => GetContainer().IsValid;
}
