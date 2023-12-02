namespace SH.Domain.Events;

public class RestoredDomainEvent : DomainEvent
{
    public RestoredDomainEvent(Type model)
    {
        string id = model.GetField("Id")?.GetValue(null)?.ToString();
        this.SetTable(model.Name, id, $"Restored {model.Name} : Id-{id} .");

        Model = model;
    }

    public Type Model { get; set; }
}