// GroomWise
// Copyright (C) 2023  John Russell C. Camo (@russkyc)
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY

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
