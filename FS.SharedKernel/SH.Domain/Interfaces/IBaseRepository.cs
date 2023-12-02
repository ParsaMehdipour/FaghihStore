namespace SH.Domain.Interfaces;

public interface IBaseRepository<TEntity> : IQueryRepository<TEntity> where TEntity : class, IAggregateRoot
{
    void Add(TEntity entity);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);

    void AddRange(IEnumerable<TEntity> entities);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

    void Update(TEntity entity);
    void UpdateRange(List<TEntity> entities);

    void Delete(TEntity entity);

    Task SaveAsync(CancellationToken cancellationToken);
    void Save();
}