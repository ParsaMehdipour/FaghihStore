using SH.Domain.Interfaces;

using SS.Domain.Models;

namespace SS.Domain.Repositories;
public interface ISitePanelSenderRepository : IBaseRepository<SitePanelSender>
{
    Task<SitePanelSender> GetFirst(CancellationToken cancellationToken);
}
