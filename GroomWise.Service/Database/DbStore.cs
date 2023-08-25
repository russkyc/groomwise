// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Linq.Expressions;
using GroomWise.Service.Database.Interfaces;
using LiteDB;

namespace GroomWise.Service.Database;

public class DbStore : IDbStore
{
    private readonly ILiteDatabase _db;

    public DbStore(string connectionString)
    {
        _db = new LiteDatabase(connectionString);
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
}
