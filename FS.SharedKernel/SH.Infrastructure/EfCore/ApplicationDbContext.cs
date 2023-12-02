using Microsoft.EntityFrameworkCore;

using SH.Application.Interfaces;
using SH.Domain;
using SH.Domain.Interfaces;

namespace SH.Infrastructure.EfCore;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    protected ICurrentUserService _currentUserService { get; }
    protected IDomainEventService _domainEventService { get; }
    protected ApplicationDbContext(DbContextOptions options,
        ICurrentUserService currentUserService,
        IDomainEventService domainEventService) : base(options)
    {
        _currentUserService = currentUserService;
        _domainEventService = domainEventService;
    }

    //public constructor for efcore
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<DomainEvent> DomainEvents { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                //TODO: createdBy and ModifiedBy will be changed.
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserId.ToString();
                    entry.Entity.CreatedDate = DateTime.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService.UserId.ToString();
                    entry.Entity.ModifiedDate = DateTime.Now;
                    break;
            }
        }

        var events = ChangeTracker.Entries<IHasDomainEvent>()
            .Select(x => x.Entity.DomainEvents)
            .SelectMany(x => x)
            .Where(domainEvent => !domainEvent.IsPublished)
            .ToArray();

        var result = await base.SaveChangesAsync(cancellationToken);

        await DispatchEvents(events);

        return result;
    }

    private async Task DispatchEvents(DomainEvent[] events)
    {
        foreach (var @event in events)
        {
            @event.SetPublished(true);
            await _domainEventService.Publish(@event);
        }
    }
}