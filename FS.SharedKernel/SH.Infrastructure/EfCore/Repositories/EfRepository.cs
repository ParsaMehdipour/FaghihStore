using Microsoft.EntityFrameworkCore;

using SH.Domain.Interfaces;

namespace SH.Infrastructure.EfCore.Repositories;

public class EfRepository<TEntity> : EfQueryRepository<TEntity>, IBaseRepository<TEntity>
                                     where TEntity : class, IAggregateRoot
{
    public EfRepository(DbContext context) : base(context)
    {
    }

    public virtual void Add(TEntity entity)
    {
        Entities.Add(entity);
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await Entities.AddAsync(entity, cancellationToken);
    }

    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        Entities.AddRange(entities);
    }

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        await Entities.AddRangeAsync(entities, cancellationToken);
    }

    public virtual void Update(TEntity entity)
    {
        Entities.Update(entity);
    }

    public virtual void UpdateRange(List<TEntity> entities)
    {
        Entities.UpdateRange(entities);
    }

    public void Delete(TEntity entity)
    {
        Entities.Remove(entity);
    }

    public virtual void Save()
    {
        Context.SaveChanges();
    }

    public virtual async Task SaveAsync(CancellationToken cancellationToken)
    {
        await Context.SaveChangesAsync(cancellationToken);
    }
}