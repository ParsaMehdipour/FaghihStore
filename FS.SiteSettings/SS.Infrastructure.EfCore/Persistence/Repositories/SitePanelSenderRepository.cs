using Microsoft.EntityFrameworkCore;
using SH.Infrastructure.EfCore.Repositories;
using SS.Domain.Models;
using SS.Domain.Repositories;

namespace SS.Infrastructure.EfCore.Persistence.Repositories;

public class SitePanelSenderRepository : EfRepository<SitePanelSender>, ISitePanelSenderRepository
{
    public SitePanelSenderRepository(SiteSettingDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<SitePanelSender> GetFirst(CancellationToken cancellationToken)
    {
        return await this.Entities.FirstOrDefaultAsync(cancellationToken);
    }
}