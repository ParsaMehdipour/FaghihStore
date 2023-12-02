using SH.Domain;
using SH.Domain.Interfaces;

namespace CD.Domain.Models;

public class CountryDivision : AuditableEntity, IAggregateRoot
{

    private CountryDivision()
    {

    }

    public string Name { get; private set; }


    public Guid? ParentId { get; private set; }
    public CountryDivision Parent { get; private set; }

    internal CountryDivision(string name)
    {
        SetName(name);
        CreatedDate = DateTime.Now;
    }

    public void SetName(string name)
    {
        if (Name == name)
            return;

        Name = name;
    }

    public void SetParent(Guid parentId)
    {
        if (ParentId == parentId)
            return;
        else if (parentId == Guid.Empty)
            ParentId = null;
        else
            ParentId = parentId;
    }
}
