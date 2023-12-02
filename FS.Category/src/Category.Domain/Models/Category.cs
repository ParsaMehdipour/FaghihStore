using SH.Domain;
using SH.Domain.Interfaces;

namespace Category.Domain.Models;

public class Category : AuditableEntity, IAggregateRoot
{

    private Category()
    {

    }

    public string Title { get; private set; }
    public string Slug { get; private set; }
    public int OrderNumber { get; private set; }
    public bool Status { get; private set; }

    public Guid? ParentCategoryId { get; private set; }
    public Category Parent { get; private set; }
    public ICollection<Category> Children { get; private set; }

    public Category(string title, string slug, bool status)
    {
        SetTitle(title);
        SetSlug(slug);
        SetStatus(status);
        CreatedDate = DateTime.Now;

        Children = new List<Category>();
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

    public void SetParent(Guid parentId)
    {
        if (ParentCategoryId == parentId)
            return;
        else if (parentId == Guid.Empty)
            return;

        ParentCategoryId = parentId;
    }
}
