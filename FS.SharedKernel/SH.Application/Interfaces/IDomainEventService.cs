using SH.Domain;

namespace SH.Application.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}