﻿using System.Linq.Expressions;

namespace SH.Domain.Interfaces;

public interface IQueryRepository<TEntity> where TEntity : class, IAggregateRoot
{
    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> Get();

    TEntity GetById(params object[] ids);
    ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
    Task<TEntity> GetWithoutQueryFilterAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    Task<bool> IsExistsAsync(CancellationToken cancellationToken);
    bool IsExists();

    Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    bool IsExists(Expression<Func<TEntity, bool>> predicate);
}