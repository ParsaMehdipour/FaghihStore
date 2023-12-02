namespace SS.Domain.Models;

public class SiteSettingFactory
{
    public SiteSetting Create(string title, string description, string mobilePhoneNumber,
        string phone, string instagram, string telegram, string whatsapp, string nationalId, string address)
    {
        SiteSetting siteSetting = new(title, description, mobilePhoneNumber, phone, instagram, telegram, whatsapp, nationalId, address);

        return siteSetting;
    }
}