using Microsoft.EntityFrameworkCore;

using SH.Infrastructure.EfCore.Repositories;

using SS.Domain.Models;
using SS.Domain.Repositories;

namespace SS.Infrastructure.EfCore.Persistence.Repositories;
public class SiteSettingRepository : EfRepository<SiteSetting>, ISiteSettingRepository
{
    public SiteSettingRepository(SiteSettingDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<SiteSetting> GetFirst(CancellationToken cancellationToken)
    {
        return await this.Entities.FirstOrDefaultAsync(cancellationToken);
    }
}