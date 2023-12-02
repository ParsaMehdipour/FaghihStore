using PM.Domain.ProductAgg;

using SH.Domain;
using SH.Domain.Interfaces;

namespace PM.Domain.ProductImageAgg;

public class ProductImage : AuditableEntity, IAggregateRoot
{
    public string Title { get; private set; }
    public string Alt { get; private set; }

    public string Url { get; private set; }
    public string FileName { get; private set; }
    public bool IsThumbnail { get; private set; }

    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }

    public ProductImage(string title, string alt, string url, string fileName, bool isThumbnail, Guid productId)
    {
        SetAlt(alt);
        SetTitle(title);
        SetUrl(url);
        SetFileName(fileName);
        SetThumbnail(isThumbnail);
        SetProductId(productId);
        CreatedDate = DateTime.Now;
    }

    public void SetTitle(string title)
    {
        if (Title == title)
            return;

        Title = title;
    }

    public void SetAlt(string alt)
    {
        if (Alt == alt)
            return;

        Alt = alt;
    }

    public void SetUrl(string url)
    {
        if (Url == url)
            return;

        Url = url;
    }

    public string GetFileNamePath()
    {
        if (!Url.StartsWith("/"))
            Url = "/" + Url;

        if (!Url.EndsWith("/"))
            Url += "/";

        return Url + FileName;
    }

    public void SetFileName(string fileName)
    {
        if (FileName == fileName)
            return;

        FileName = fileName;
    }

    public void SetThumbnail(bool isThumbnail)
    {
        if (IsThumbnail == isThumbnail)
            return;

        IsThumbnail = isThumbnail;
    }

    public void SetProductId(Guid productId)
    {
        if (ProductId == productId)
            return;

        ProductId = productId;
    }

    public void SetModifiedDate(DateTime modifiedDate)
    {
        if (ModifiedDate == modifiedDate)
            return;

        ModifiedDate = modifiedDate;
    }
}