using SH.Domain.Interfaces;

using SS.Domain.Models;

namespace SS.Domain.Repositories;
public interface ISiteSettingRepository : IBaseRepository<SiteSetting>
{
    Task<SiteSetting> GetFirst(CancellationToken cancellationToken);
}