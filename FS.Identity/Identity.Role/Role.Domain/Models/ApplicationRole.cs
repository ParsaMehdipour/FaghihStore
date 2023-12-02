using Microsoft.AspNetCore.Identity;

using SH.Domain.Interfaces;

namespace Role.Domain.Models;

public class ApplicationRole : IdentityRole<Guid>, IAggregateRoot
{
    private ApplicationRole()
    {

    }
    public string DisplayName { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public Guid? CreatedBy { get; private set; }
    public DateTime? ModifiedDate { get; private set; }
    public Guid? LastModifiedBy { get; private set; }
    public bool IsDeleted { get; private set; }

    public ApplicationRole(string name, string displayName, Guid? createdBy)
    {
        SetName(name);
        SetDisplayName(displayName);
        SetCreatedBy(createdBy);
        CreatedDate = DateTime.Now;
    }

    public void SetName(string name)
    {
        if (Name == name)
            return;

        Name = name;
    }

    public void SetDisplayName(string displayName)
    {
        if (DisplayName == displayName)
            return;

        DisplayName = displayName;
    }

    public void SetCreatedBy(Guid? createdBy)
    {
        if (CreatedBy != createdBy)
            CreatedBy = createdBy;
    }

    public void SetModifiedDate(DateTime modifiedDate, Guid? lastModifiedBy)
    {
        if (ModifiedDate != modifiedDate)
            ModifiedDate = modifiedDate;

        if (LastModifiedBy != lastModifiedBy)
            LastModifiedBy = lastModifiedBy;
    }

    public void DeleteItem()
    {
        IsDeleted = true;
    }

    public void RestoreItem()
    {
        IsDeleted = false;
    }
}
