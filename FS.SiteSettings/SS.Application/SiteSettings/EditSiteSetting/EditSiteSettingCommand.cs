using Microsoft.AspNetCore.Http;
using SS.Domain.Models;

namespace SS.Application.SiteSettings.EditSiteSetting;

public record EditSiteSettingCommand(string Title, string Description,
    string MobilePhoneNumber, string Phone, string NationalId, string Address)
{
    public IFormFile Logo { get; init; }
    public IFormFile Favicon { get; init; }
    public string CurrentLogo { get; init; }
    public string CurrentFavicon { get; init; }
    public string Instagram { get; init; }
    public string Telegram { get; init; }
    public string Whatsapp { get; init; }
}

public static class EditSiteSettingCommandExtension
{
    public static EditSiteSettingCommand ToCommand(this SiteSetting siteSetting)
    {
        return new(siteSetting.Title,
            siteSetting.Description,
            siteSetting.MobilePhoneNumber,
            siteSetting.Phone,
            siteSetting.NationalId,
            siteSetting.Address)
        {
            CurrentLogo = siteSetting.Logo,
            CurrentFavicon = siteSetting.Favicon,
            Instagram = siteSetting.Instagram,
            Telegram = siteSetting.Telegram,
            Whatsapp = siteSetting.Whatsapp
        };
    }
}