using Microsoft.AspNetCore.Http;

namespace SS.Application.SiteSettings.CreateSiteSetting;

public record CreateSiteSettingCommand(string Title, string Description, string MobilePhoneNumber, string Phone, string NationalId, string Address)
{
    public IFormFile Logo { get; init; }
    public IFormFile Favicon { get; init; }
    public string Instagram { get; init; }
    public string Telegram { get; init; }
    public string Whatsapp { get; init; }
}