// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Linq.Expressions;

namespace GroomWise.Infrastructure.Data.Interfaces;

public interface IRepository<TEntity>
{
    Guid Create(TEntity entity);
    TEntity Get(Guid id);
    IEnumerable<TEntity>? GetAll();
    int AddRange(IEnumerable<TEntity> entities);
    bool Update(TEntity entity);
    int UpdateRange(
        Expression<Func<TEntity, TEntity>> modifyAction,
        Expression<Func<TEntity, bool>> filter
    );
    bool Delete(Guid id);
    int DeleteRange(Expression<Func<TEntity, bool>> filter);
    TEntity? Search(Expression<Func<TEntity, bool>> filter);
    IEnumerable<TEntity>? FindAll(
        Expression<Func<TEntity, bool>> filter,
        int skip = 0,
        int limit = Int32.MaxValue
    );
}
