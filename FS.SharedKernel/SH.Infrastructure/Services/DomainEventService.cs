using MediatR;

using Microsoft.Extensions.Logging;

using SH.Application.Interfaces;
using SH.Application.Models;
using SH.Domain;
using SH.Infrastructure.EfCore;

namespace SH.Infrastructure.Services;

public class DomainEventService : IDomainEventService
{
    protected ILoggerFactory _logger { get; }
    protected ApplicationDbContext _dbContext { get; }
    protected IPublisher _mediator { get; }
    public DomainEventService(IPublisher mediator,
        ILoggerFactory logger,
        ApplicationDbContext dbContext)
    {
        _mediator = mediator;
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task Publish(DomainEvent domainEvent)
    {
        await _dbContext.AddAsync(domainEvent, default);

        var result = await _dbContext.SaveChangesAsync(default);

        if (result is 1)
            _logger.CreateLogger("Event").LogInformation(domainEvent.Description);

        await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
    }

    private INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
    {
        return (INotification)Activator.CreateInstance(
            typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent)!;
    }
}
