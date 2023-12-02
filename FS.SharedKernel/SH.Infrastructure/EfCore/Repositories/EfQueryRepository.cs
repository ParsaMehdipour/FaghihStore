using Microsoft.EntityFrameworkCore;

using SH.Domain.Interfaces;

using System.Linq.Expressions;

namespace SH.Infrastructure.EfCore.Repositories;

public class EfQueryRepository<TEntity> : IQueryRepository<TEntity> where TEntity : class, IAggregateRoot
{
    protected DbSet<TEntity> Entities { get; }
    protected IQueryable<TEntity> Table => Entities;
    protected IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

    protected DbContext Context { get; }

    public EfQueryRepository(DbContext context)
    {
        Context = context;
        Entities = Context.Set<TEntity>();
    }

    /// <summary>
    /// list of <see cref="TEntity"/> by no tracking.
    /// </summary>
    /// <returns></returns>
    public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
    {
        return TableNoTracking.Where(predicate);
    }

    public IQueryable<TEntity> Get()
    {
        return TableNoTracking;
    }

    /// <summary>
    /// find item in <see cref="TEntity"/> by tracking.
    /// </summary>
    /// <returns></returns>
    public virtual TEntity GetById(params object[] ids)
    {
        return Entities.Find(ids);
    }

    /// <summary>
    /// find async item in <see cref="TEntity"/> by tracking.
    /// </summary>
    /// <returns></returns>
    public virtual ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
    {
        return Entities.FindAsync(ids, cancellationToken);
    }

    public async Task<TEntity> GetWithoutQueryFilterAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return await Entities.IgnoreQueryFilters().SingleOrDefaultAsync(predicate, cancellationToken);
    }

    /// <summary>
    /// is exists this <see cref="TEntity"/> in database by no tracking.
    /// </summary>
    /// <returns></returns>
    public virtual bool IsExists()
    {
        return TableNoTracking.Any();
    }

    /// <summary>
    /// is exists async this <see cref="TEntity"/> in database by no tracking.
    /// </summary>
    /// <returns></returns>
    public virtual async Task<bool> IsExistsAsync(CancellationToken cancellationToken)
    {
        return await TableNoTracking.AnyAsync(cancellationToken);
    }

    /// <summary>
    /// is exists this <see cref="TEntity"/> in database by no tracking.
    /// </summary>
    /// <returns></returns>
    public virtual bool IsExists(Expression<Func<TEntity, bool>> predicate)
    {
        return TableNoTracking.Any(predicate);
    }

    /// <summary>
    /// is exists async this <see cref="TEntity"/> in database by no tracking.
    /// </summary>
    /// <returns></returns>
    public virtual async Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return await TableNoTracking.AnyAsync(predicate, cancellationToken);
    }
}