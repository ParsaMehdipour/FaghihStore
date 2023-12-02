using SH.Domain;
using SH.Domain.Interfaces;

namespace TG.Domain.Models;

public class Trait : AuditableEntity, IAggregateRoot
{
    public string Title { get; private set; }
    public int OrderNumber { get; private set; }
    public bool HasFilterAbility { get; private set; }

    public Guid CategoryId { get; private set; }
    public Guid TraitGroupId { get; private set; }
    public TraitGroup TraitGroup { get; private set; }

    internal Trait(string title, int orderNumber, Guid traitGroupId, Guid categoryId, bool hasFilterAbility)
    {
        SetTitle(title);
        SetOrderNumber(orderNumber);
        SetTraitGroupId(traitGroupId);
        SetCategoryId(categoryId);
        SetHasFilterAbility(hasFilterAbility);
        CreatedDate = DateTime.Now;
    }

    public void SetHasFilterAbility(bool hasFilterAbility)
    {
        if (HasFilterAbility == hasFilterAbility)
            return;

        HasFilterAbility = hasFilterAbility;
    }

    public void SetTitle(string title)
    {
        if (Title == title)
            return;

        Title = title;
    }

    public void SetOrderNumber(int orderNumber)
    {
        if (OrderNumber == orderNumber)
            return;
        OrderNumber = orderNumber;
    }

    public void SetTraitGroupId(Guid traitGroupId)
    {
        if (TraitGroupId == traitGroupId)
            return;

        TraitGroupId = traitGroupId;
    }

    public void SetCategoryId(Guid categoryId)
    {
        if (CategoryId == categoryId)
            return;

        CategoryId = categoryId;
    }

    public void SetModifiedDate(DateTime modifiedDate)
    {
        if (ModifiedDate != modifiedDate)
            ModifiedDate = modifiedDate;
    }
}