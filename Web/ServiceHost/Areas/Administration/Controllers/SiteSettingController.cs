using SS.Application.Interfaces;
using SS.Application.SiteSettings.EditSiteSetting;
using SS.Infrastructure.EfCore;

namespace ServiceHost.Areas.Administration.Controllers;

[Authorize(Policy = SiteSettingManagementPermissionExposer.Permissions.SITESETTING_FullManagement)]
public class SiteSettingController : AdminController
{
    protected ISiteSettingService _siteSettingService { get; }

    public SiteSettingController(ISiteSettingService siteSettingService)
    {
        _siteSettingService = siteSettingService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var siteSetting = await _siteSettingService.Single(cancellationToken);

        return View(siteSetting.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Index(EditSiteSettingCommand command, CancellationToken cancellationToken)
    {
        var result = await _siteSettingService.Edit(command, cancellationToken);

        if (result.IsFailed)
        {
            ModelState.AddModelError(string.Empty, result.Errors.Select(_ => _.Message).FirstOrDefault());
            return View(command);  //TODO: will be notify by js
        }

        return RedirectToAction();
    }
}