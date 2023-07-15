// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Linq.Expressions;
using GroomWise.Infrastructure.Data.Interfaces;

namespace GroomWise.Infrastructure.Data;

public class RepositoryProvider<TEntity> : IRepository<TEntity>
{
    private readonly IDataAccessProvider _dataAccessProvider;

    public RepositoryProvider(IDataAccessProvider dataAccessProvider)
    {
        _dataAccessProvider = dataAccessProvider;
    }
    
    public Guid Create(TEntity entity)
    {
        return _dataAccessProvider.Add(entity);
    }

    public TEntity Get(Guid id)
    {
        return _dataAccessProvider.Get<TEntity>(id);
    }

    public IEnumerable<TEntity>? GetAll()
    {
        return _dataAccessProvider.GetAll<TEntity>();
    }

    public int AddRange(IEnumerable<TEntity> entities)
    {
        return _dataAccessProvider.AddMany(entities);
    }

    public bool Update(TEntity entity)
    {
        return _dataAccessProvider.Update(entity);
    }

    public int UpdateRange(Expression<Func<TEntity, TEntity>> modifyAction, Expression<Func<TEntity, bool>> filter)
    {
        return _dataAccessProvider.UpdateMany(modifyAction, filter);
    }

    public bool Delete(Guid id)
    {
        return _dataAccessProvider.Delete<TEntity>(id);
    }

    public int DeleteRange(Expression<Func<TEntity, bool>> filter)
    {
        return _dataAccessProvider.DeleteMany(filter);
    }

    public TEntity? Search(Expression<Func<TEntity, bool>> filter)
    {
        return _dataAccessProvider.Find(filter);
    }

    public IEnumerable<TEntity> FindAll(
        Expression<Func<TEntity, bool>> filter,
        int skip = 0,
        int limit = Int32.MaxValue)
    {
        return _dataAccessProvider.FindMany(filter, skip, limit);
    }
}