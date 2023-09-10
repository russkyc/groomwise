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
using Injectio.Attributes;
using LiteDB;

namespace GroomWise.Infrastructure.Database;

[RegisterSingleton<IDbStore, LiteDbStore>]
public class LiteDbStore : IDbStore
{
    private readonly ILiteDatabase _db;

    public LiteDbStore()
    {
        _db = new LiteDatabase("data.db");
    }

    public LiteDbStore(Stream stream)
    {
        _db = new LiteDatabase(stream);
    }

    public Guid Insert<T>(T entity)
        where T : class
    {
        return _db.GetCollection<T>().Insert(entity).AsGuid;
    }

    public int InsertMany<T>(IEnumerable<T> entities)
        where T : class
    {
        return _db.GetCollection<T>().InsertBulk(entities);
    }

    public T? Get<T>(Expression<Func<T, bool>> filter)
        where T : class
    {
        return _db.GetCollection<T>().FindOne(filter);
    }

    public IEnumerable<T> GetMultiple<T>(
        Expression<Func<T, bool>> filter,
        int skip = 0,
        int limit = Int32.MaxValue
    )
        where T : class
    {
        return _db.GetCollection<T>().Query().Where(filter).Skip(skip).Limit(limit).ToEnumerable();
    }

    public bool Update<T>(Guid id, T entity)
        where T : class
    {
        return _db.GetCollection<T>().Update(id, entity);
    }

    public int UpdateMultiple<T>(Expression<Func<T, T>> extend, Expression<Func<T, bool>> filter)
        where T : class
    {
        return _db.GetCollection<T>().UpdateMany(extend, filter);
    }

    public bool Delete<T>(Guid id)
        where T : class
    {
        return _db.GetCollection<T>().Delete(id);
    }

    public int DeleteMultiple<T>(Expression<Func<T, bool>> filter)
        where T : class
    {
        return _db.GetCollection<T>().DeleteMany(filter);
    }

    public IEnumerable<T> GetAll<T>()
    {
        return _db.GetCollection<T>().Query().ToEnumerable();
    }
}
