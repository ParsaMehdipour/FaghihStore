namespace SH.Domain;

public class DomainEvent : AuditableEntity<long>
{
    public string TableName { get; private set; }
    public string TableId { get; private set; }

    public string Description { get; private set; }

    public bool IsPublished { get; private set; }
    public DateTimeOffset DateOccurred { get; private set; }

    protected DomainEvent()
    {
        DateOccurred = DateTimeOffset.UtcNow;
        IsPublished = false;
    }

    public void SetTable(string name, string id, string description)
    {
        TableName = name;
        TableId = id;
        Description = description;
    }

    public void SetPublished(bool isPublish)
    {
        IsPublished = isPublish;
    }
}