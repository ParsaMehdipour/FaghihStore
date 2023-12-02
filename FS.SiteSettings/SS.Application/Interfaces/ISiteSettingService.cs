using SS.Application.SiteSettings.CreateSiteSetting;
using SS.Application.SiteSettings.EditSiteSetting;
using SS.Domain.Models;

namespace SS.Application.Interfaces;
public interface ISiteSettingService
{
    Task<Result<EditSiteSettingCommand>> Single(CancellationToken cancellationToken);
    Task<Result<SiteSetting>> Add(CreateSiteSettingCommand command, CancellationToken cancellationToken);
    Task<Result> Edit(EditSiteSettingCommand command, CancellationToken cancellationToken);
}