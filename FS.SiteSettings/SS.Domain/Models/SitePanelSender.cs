using SH.Domain;
using SH.Domain.Interfaces;

using SS.Domain.Enums;

namespace SS.Domain.Models;

/// <summary>
/// using this model to config panel sender messages from system to user or admin.
/// </summary>
public class SitePanelSender : AuditableEntity, IAggregateRoot
{
    private SitePanelSender()
    {
    }

    public string Username { get; private set; }
    public string Password { get; set; }
    //public string SMSKeyToken { get; private set; } TODO: I think don't need this token property.
    public SenderType SenderType { get; private set; }
    public bool IsEnabled { get; private set; }

    internal SitePanelSender(string username, string password, SenderType senderType, bool isEnabled)
    {
        SetUsername(username);
        SetPassword(password);
        SetSenderType(senderType);
        SetPanelSenderStatus(isEnabled);
    }

    public void SetUsername(string username)
    {
        if (Username == username)
            return;

        Username = username;
    }

    public void SetPassword(string password)
    {
        if (Password == password)
            return;

        Password = password;
    }

    public void SetSenderType(SenderType senderType)
    {
        if (SenderType == senderType)
            return;

        SenderType = senderType;
    }

    public void SetPanelSenderStatus(bool status)
    {
        if (IsEnabled == status)
            return;

        IsEnabled = status;
    }
}