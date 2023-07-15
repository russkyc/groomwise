// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Linq.Expressions;

namespace GroomWise.Infrastructure.Data.Interfaces;

public interface IDataAccessProvider
{
    Guid Add<TEntity>(TEntity entity);
    TEntity Get<TEntity>(Guid id);
    IEnumerable<TEntity>? GetAll<TEntity>();
    int AddMany<TEntity>(IEnumerable<TEntity> collection);
    bool Update<TEntity>(TEntity entity);
    int UpdateMany<TEntity>(Expression<Func<TEntity, TEntity>> action, Expression<Func<TEntity, bool>> filter);
    bool Delete<TEntity>(Guid id);
    int DeleteMany<TEntity>(Expression<Func<TEntity, bool>> filter);
    TEntity? Find<TEntity>(Expression<Func<TEntity, bool>> filter);
    IEnumerable<TEntity> FindMany<TEntity>(
        Expression<Func<TEntity, bool>>? filter,
        int skip = 0,
        int limit = Int32.MaxValue
    );
}
