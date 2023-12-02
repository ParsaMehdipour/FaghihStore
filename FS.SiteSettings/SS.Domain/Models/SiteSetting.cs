using SH.Domain;
using SH.Domain.Interfaces;

namespace SS.Domain.Models;

public class SiteSetting : AuditableEntity, IAggregateRoot
{
    private SiteSetting()
    {
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Logo { get; private set; }
    public string Favicon { get; private set; }
    public string MobilePhoneNumber { get; private set; }
    public string Phone { get; private set; }
    public string NationalId { get; private set; }
    public string Address { get; private set; }

    //Social Medias
    public string Instagram { get; private set; }
    public string Telegram { get; private set; }
    public string Whatsapp { get; private set; }

    internal SiteSetting(string title, string description, string mobilePhoneNumber, string phone, string instagram, string telegram, string whatsapp, string nationalId, string address)
    {
        SetTitle(title);
        SetDescription(description);
        SetMobilePhoneNumber(mobilePhoneNumber);
        SetPhone(phone);
        SetNationalId(nationalId);
        SetAddress(address);
        SetInstagram(instagram);
        SetTelegram(telegram);
        SetWhatsapp(whatsapp);
        CreatedDate = DateTime.Now;
    }

    public void SetTitle(string title)
    {
        if (Title == title)
            return;

        Title = title;
    }

    public void SetDescription(string description)
    {
        if (Description == description)
            return;

        Description = description;
    }

    public void SetLogo(string logo)
    {
        if (Logo == logo)
            return;

        Logo = logo;
    }

    public void SetFavicon(string favicon)
    {
        if (Favicon == favicon)
            return;

        Favicon = favicon;
    }

    public void SetMobilePhoneNumber(string mobilePhoneNumber)
    {
        if (MobilePhoneNumber == mobilePhoneNumber)
            return;

        MobilePhoneNumber = mobilePhoneNumber;
    }

    public void SetPhone(string phone)
    {
        if (Phone == phone)
            return;

        Phone = phone;
    }

    public void SetNationalId(string nationalId)
    {
        if (NationalId == nationalId)
            return;

        NationalId = nationalId;
    }

    public void SetAddress(string address)
    {
        if (Address == address)
            return;

        Address = address;
    }

    public void SetInstagram(string instagram)
    {
        if (Instagram == instagram)
            return;

        Instagram = instagram;
    }

    public void SetTelegram(string telegram)
    {
        if (Telegram == telegram)
            return;

        Telegram = telegram;
    }

    public void SetWhatsapp(string whatsapp)
    {
        if (Whatsapp == whatsapp)
            return;

        Whatsapp = whatsapp;
    }
}