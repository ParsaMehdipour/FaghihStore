using SH.Application.Interfaces;
using SS.Application.Interfaces;
using SS.Application.SiteSettings.CreateSiteSetting;
using SS.Application.SiteSettings.EditSiteSetting;
using SS.Domain.Models;

namespace SS.Application.SiteSettings;

public class SiteSettingService : ISiteSettingService
{
    public const string SITESETTING_UPLOAD_PATH = "SiteSetting";

    protected ISiteSettingRepository _siteSettingRepository { get; }
    protected SiteSettingFactory _siteSettingFactory { get; }
    protected ISiteSettingValidatorService _siteSettingValidatorService { get; }
    protected IFileUploaderService _fileUploaderService { get; }

    public SiteSettingService(ISiteSettingRepository siteSettingRepository,
        SiteSettingFactory siteSettingFactory,
        ISiteSettingValidatorService siteSettingValidatorService,
        IFileUploaderService fileUploaderService)
    {
        _siteSettingRepository = siteSettingRepository;
        _siteSettingFactory = siteSettingFactory;
        _siteSettingValidatorService = siteSettingValidatorService;
        _fileUploaderService = fileUploaderService;
    }

    public async Task<Result<EditSiteSettingCommand>> Single(CancellationToken cancellationToken)
    {
        var siteSetting = await _siteSettingRepository.GetFirst(cancellationToken);

        if (siteSetting is null)
        {
            var createResult = await this.Add(new("عنوان", "توضیحات", "09123456789", "011234567890",
                  "1234567890", "آدرس")
            {
                Logo = null,
                Favicon = null,
                Instagram = "instagram.com",
                Telegram = "telegram.org",
                Whatsapp = "whatsapp.com"
            }, cancellationToken);

            if (createResult.IsFailed)
                return Result.Fail(createResult.Errors.Select(_ => _.Message).FirstOrDefault());

            siteSetting = createResult.Value;
        }

        return Result.Ok(siteSetting.ToCommand());

    }

    public async Task<Result<SiteSetting>> Add(CreateSiteSettingCommand command, CancellationToken cancellationToken)
    {
        var result = await _siteSettingValidatorService.CreateValidation(command, cancellationToken);

        if (result.IsFailed)
            return Result.Fail(result.Errors.Select(_ => _.Message).FirstOrDefault());

        SiteSetting siteSetting = _siteSettingFactory.Create(command.Title, command.Description, command.MobilePhoneNumber, command.Phone,
             command.Instagram, command.Telegram, command.Whatsapp, command.NationalId, command.Address);

        if (command.Logo is not null)
            siteSetting.SetLogo(await _fileUploaderService.Upload(command.Logo, cancellationToken, SITESETTING_UPLOAD_PATH));

        if (command.Favicon is not null)
            siteSetting.SetFavicon(await _fileUploaderService.Upload(command.Favicon, cancellationToken, SITESETTING_UPLOAD_PATH));

        await _siteSettingRepository.AddAsync(siteSetting, cancellationToken);
        await _siteSettingRepository.SaveAsync(cancellationToken);

        return Result.Ok(siteSetting);
    }

    public async Task<Result> Edit(EditSiteSettingCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _siteSettingValidatorService.EditValidation(command, cancellationToken);

        if (validationResult.IsFailed)
            return Result.Fail(validationResult.Errors.Select(_ => _.Message).FirstOrDefault());

        SiteSetting siteSetting = await _siteSettingRepository.GetFirst(cancellationToken);

        if (siteSetting is null)
            return Result.Fail("SiteSetting not found");

        siteSetting.SetTitle(command.Title);
        siteSetting.SetDescription(command.Description);
        siteSetting.SetAddress(command.Address);
        siteSetting.SetNationalId(command.NationalId);
        siteSetting.SetMobilePhoneNumber(command.MobilePhoneNumber);
        siteSetting.SetPhone(command.Phone);

        siteSetting.SetInstagram(command.Instagram);
        siteSetting.SetTelegram(command.Telegram);
        siteSetting.SetWhatsapp(command.Whatsapp);

        if (command.Logo is not null)
        {
            //TODO: before upload new logo, should be delete old logo in directory.

            siteSetting.SetLogo(await _fileUploaderService.Upload(command.Logo, cancellationToken, SITESETTING_UPLOAD_PATH));
        }

        if (command.Favicon is not null)
        {
            //TODO: before upload new favicon, should be delete old favicon in directory.

            siteSetting.SetFavicon(await _fileUploaderService.Upload(command.Favicon, cancellationToken, SITESETTING_UPLOAD_PATH));
        }

        await _siteSettingRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }
}