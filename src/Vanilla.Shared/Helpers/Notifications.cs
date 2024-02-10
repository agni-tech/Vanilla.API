using Vanilla.Shared.Interfaces;
using MySaviors.Helpers.Extensions;

namespace Vanilla.Shared.Helpers;

public class Notifications : INotifications
{
    #region Attributes
    public List<string> Messages { get; }

    #endregion

    #region Constructor

    public Notifications()
        => Messages = new List<string>();

    #endregion

    #region Methods

    public static Notifications Init()
        => new();

    public Notifications When(bool exists, string msg)
    {
        if (exists)
            AddMessage(msg);

        return this;
    }
    public bool IsValid
        => !Messages.HaveAny();
    public void AddMessage(string message)
        => Messages.Add(message);
    public void AddError(string caller, Exception ex)
        => Messages.Add($"[{caller}] => {ex.Message} :: {ex.InnerException}");

    #endregion
}
