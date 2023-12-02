using SS.Domain.Enums;
using SS.Domain.Models;

namespace SS.Application.SiteSenders.EditSitePanelSender;

public record EditSitePanelSenderCommand(string UserName, string Password, SenderType SenderType, bool Status);

public static class EditSitePanelSenderExtension
{
    public static EditSitePanelSenderCommand ToCommand(this SitePanelSender sitePanelSender)
    {
        return new(
            sitePanelSender.Username,
            sitePanelSender.Password,
            sitePanelSender.SenderType,
            sitePanelSender.IsEnabled);
    }
}
