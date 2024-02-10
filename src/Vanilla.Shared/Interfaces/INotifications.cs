namespace Vanilla.Shared.Interfaces
{
    public interface INotifications
    {
        List<string> Messages { get; }
        bool IsValid { get; }
        void AddMessage(string message);
        void AddError(string caller, Exception ex);
    }

}