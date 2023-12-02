using Identity.Infrastructure.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using SH.Infrastructure.Consts;
using SH.Infrastructure.Settings;

using User.Application.Users.Commands.RegisterUser;

namespace Identity.Controllers;

[Route("[action]")]
public class AuthController : Controller
{
    [TempData]
    public string CallBackUrl { get; set; } = "/";

    protected IAuthManager _authManager { get; }

    public AuthController(IAuthManager authManager)
    {
        _authManager = authManager;
    }

    [HttpGet]
    public IActionResult Register([FromQuery] string callBackUrl)
    {
        CallBackUrl = callBackUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserCommand command, CancellationToken cancellationToken, string callBackUrl)
    {
        TempData.Keep(nameof(CallBackUrl));

        var response = await _authManager.Register(command, cancellationToken);

        if (response.IsFailed)
        {
            ModelState.AddModelError(nameof(command.PhoneNumber), response.Errors.Select(_ => _.Message).FirstOrDefault());
            return View(command);
        }

        return View("Verification", new VerificationCodeDto(command.PhoneNumber, string.Empty));
    }

    [HttpPost]
    public async Task<IActionResult> Verification(VerificationCodeDto dto, CancellationToken cancellationToken, string callBackUrl)
    {
        TempData.Keep(nameof(CallBackUrl));

        if (string.IsNullOrWhiteSpace(dto.VerificationCode))
        {
            ModelState.AddModelError(nameof(dto.VerificationCode), ValidationMessage.VerificationCodeNotEmpty);
            return View("Verification", dto);
        }

        var result = await _authManager.VerifyAndSignInUser(dto, cancellationToken);

        if (result.IsFailed)
        {
            ModelState.AddModelError(nameof(dto.VerificationCode), result.Errors.Select(_ => _.Message).FirstOrDefault());
            return View("Verification", dto);
        }

        if (string.IsNullOrWhiteSpace(CallBackUrl))
            CallBackUrl = "/";

        callBackUrl = CallBackUrl;
        TempData.Remove(nameof(CallBackUrl));

        return RedirectPermanent(callBackUrl);
    }

    public async Task<IActionResult> Logout([FromServices] IOptions<SiteSettings> options)
    {
        await _authManager.SignOut();

        return RedirectPermanent(options.Value.ProjectsUrls.FirstOrDefault(projectUrl => projectUrl.Project == "ServiceHost").Url);
    }
}