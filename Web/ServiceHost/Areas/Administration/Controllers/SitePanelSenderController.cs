using SS.Application.Interfaces;
using SS.Application.SiteSenders.EditSitePanelSender;
using SS.Infrastructure.EfCore;

namespace ServiceHost.Areas.Administration.Controllers;

[Authorize(Policy = SiteSettingManagementPermissionExposer.Permissions.SITEPANELSENDER_FullManagement)]
public class SitePanelSenderController : Controller
{

    public ISitePanelSenderService _sitePanelSenderService { get; }

    public SitePanelSenderController(ISitePanelSenderService sitePanelSenderService)
    {
        _sitePanelSenderService = sitePanelSenderService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var sitePanelSender = await _sitePanelSenderService.Single(cancellationToken);

        return View(sitePanelSender.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Index(EditSitePanelSenderCommand command, CancellationToken cancellationToken)
    {
        var result = await _sitePanelSenderService.Edit(command, cancellationToken);

        if (result.IsFailed)
        {
            ModelState.AddModelError(string.Empty, result.Errors.Select(_ => _.Message).FirstOrDefault());
            return View(command);  //TODO: will be notify by js
        }

        return RedirectToAction();
    }
}
