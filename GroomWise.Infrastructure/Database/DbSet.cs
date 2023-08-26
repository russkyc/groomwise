// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Linq.Expressions;
using GroomWise.Infrastructure.Database.Interfaces;

namespace GroomWise.Infrastructure.Database;

public class DbSet<T> : IDbSet<T>
    where T : class
{
    private readonly IDbStore _dbStore;

    public DbSet(IDbStore dbStore)
    {
        _dbStore = dbStore;
    }

    public Guid Insert(T entity)
    {
        return _dbStore.Insert(entity);
    }

    public int InsertMany(IEnumerable<T> entities)
    {
        return _dbStore.InsertMany(entities);
    }

    public T? Get(Expression<Func<T, bool>> filter)
    {
        return _dbStore.Get(filter);
    }

    public IEnumerable<T> GetMultiple(
        Expression<Func<T, bool>> filter,
        int skip = 0,
        int limit = Int32.MaxValue
    )
    {
        return _dbStore.GetMultiple(filter, skip, limit);
    }

    public bool Update(Guid id, T entity)
    {
        return _dbStore.Update(id, entity);
    }

    public int UpdateMultiple(Expression<Func<T, T>> extend, Expression<Func<T, bool>> filter)
    {
        return _dbStore.UpdateMultiple(extend, filter);
    }

    public bool Delete(Guid id)
    {
        return _dbStore.Delete<T>(id);
    }

    public int DeleteMultiple(Expression<Func<T, bool>> filter)
    {
        return _dbStore.DeleteMultiple(filter);
    }

    public IEnumerable<T> GetAll()
    {
        return _dbStore.GetAll<T>();
    }
}
