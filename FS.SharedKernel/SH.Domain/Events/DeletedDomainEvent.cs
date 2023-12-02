namespace SH.Domain.Events;

public class DeletedDomainEvent : DomainEvent
{
    public DeletedDomainEvent(Type model)
    {
        string id = model.GetField("Id")?.GetValue(null)?.ToString();
        this.SetTable(model.Name, id, $"Deleted {model.Name} : Id-{id} .");

        Model = model;
    }

    public Type Model { get; set; }
}