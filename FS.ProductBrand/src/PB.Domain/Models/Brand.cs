using SH.Domain;
using SH.Domain.Interfaces;

namespace PB.Domain.Models;

public class Brand : AuditableEntity, IAggregateRoot
{
    private Brand()
    {

    }

    public string Title { get; private set; }
    public string Slug { get; private set; }
    public int OrderNumber { get; private set; }
    public bool Status { get; private set; }

    internal Brand(string title, int orderNumber, string slug, bool status)
    {
        SetTitle(title);
        SetSlug(slug);
        SetOrderNumber(orderNumber);
        SetStatus(status);
        CreatedDate = DateTime.Now;
    }

    public void SetTitle(string title)
    {
        if (Title == title)
            return;

        Title = title;
    }

    public void SetSlug(string slug)
    {
        if (Slug == slug)
            return;

        Slug = slug;
    }

    public void SetOrderNumber(int orderNumber)
    {
        if (OrderNumber == orderNumber)
            return;

        OrderNumber = orderNumber;
    }

    public void SetStatus(bool status)
    {
        if (Status == status)
            return;

        Status = status;
    }

    public void SetModifiedDate(DateTime dateTime)
    {
        ModifiedDate = dateTime;
    }

    public void DeleteBrand()
    {
        this.DeleteItem();
    }

    public void RestoreBrand()
    {
        this.RestoreItem();
    }
}