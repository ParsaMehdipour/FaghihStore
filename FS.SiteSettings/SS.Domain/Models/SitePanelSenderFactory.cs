using SS.Domain.Enums;

namespace SS.Domain.Models;
public class SitePanelSenderFactory
{
    public SitePanelSender Create(string userName, string password, SenderType senderType, bool isEnabled)
    {
        var sender = new SitePanelSender(userName, password, senderType, isEnabled);

        return sender;
    }
}
