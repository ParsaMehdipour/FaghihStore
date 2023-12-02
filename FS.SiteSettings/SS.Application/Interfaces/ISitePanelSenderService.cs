using SS.Application.SiteSenders.CreateSitePanelSender;
using SS.Application.SiteSenders.EditSitePanelSender;
using SS.Domain.Models;

namespace SS.Application.Interfaces;

public interface ISitePanelSenderService
{
    Task<Result<EditSitePanelSenderCommand>> Single(CancellationToken cancellationToken);
    Task<Result<SitePanelSender>> Add(CreateSitePanelSenderCommand command, CancellationToken cancellationToken);
    Task<Result> Edit(EditSitePanelSenderCommand command, CancellationToken cancellationToken);
    Task<Result> Delete(Guid Id, CancellationToken cancellationToken);
}