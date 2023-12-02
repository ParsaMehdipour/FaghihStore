using SH.Domain;
using SH.Domain.Interfaces;

namespace VG.Domain.Models;
public class VarietyGroup : AuditableEntity, IAggregateRoot
{
    public VarietyGroup()
    {

    }

    public VarietyGroup(string title)
    {
        SetTitle(title: title);
        ProductVarieties = new List<Variety>();

        CreatedDate = DateTime.Now;
    }

    public string Title { get; private set; }

    #region Navigation Properties

    public ICollection<Variety> ProductVarieties { get; private set; }

    #endregion

    public void SetTitle(string title)
    {
        if (Title == title)
            return;

        Title = title;
    }
}
