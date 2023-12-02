using SH.Domain;
using SH.Domain.Interfaces;
using VG.Domain.Enums;

namespace VG.Domain.Models;

public class Variety : AuditableEntity, IAggregateRoot
{
    public Variety()
    {

    }

    public string Title { get; private set; }
    public string ColorCode { get; private set; }
    public string Size { get; private set; }
    public BoxType BoxType { get; private set; }

    #region Navigation Properties

    public Guid VarietyGroupId { get; private set; }
    public VarietyGroup VarietyGroup { get; private set; }

    #endregion

    public Variety(string title, string colorCode, string size, BoxType boxType, Guid varietyGroupId)
    {
        SetTitle(title);
        SetColorCode(colorCode);
        SetSize(size);
        SetBoxType(boxType);
        SetVarietyGroupId(varietyGroupId);

        CreatedDate = DateTime.Now;
    }

    public void SetTitle(string title)
    {
        if (Title == title)
            return;
        Title = title;
    }

    public void SetColorCode(string colorCode)
    {
        if (ColorCode == colorCode)
            return;
        ColorCode = colorCode;
    }

    public void SetSize(string size)
    {
        if (Size == size)
            return;
        Size = size;
    }

    public void SetBoxType(BoxType boxType)
    {
        if (BoxType == boxType)
            return;
        BoxType = boxType;
    }

    public void SetVarietyGroupId(Guid varietyGroupId)
    {
        if (VarietyGroupId == varietyGroupId)
            return;
        if (varietyGroupId == Guid.Empty)
            return;
        VarietyGroupId = varietyGroupId;
    }
}
