using SS.Domain.Enums;

namespace SS.Application.SiteSenders.CreateSitePanelSender;

public record CreateSitePanelSenderCommand(string UserName, string Password, SenderType SenderType, bool IsEnabled);
