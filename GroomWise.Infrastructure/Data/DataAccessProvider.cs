// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Linq.Expressions;
using GroomWise.Infrastructure.Data.Interfaces;
using LiteDB;

namespace GroomWise.Infrastructure.Data;

public class DataAccessProvider : IDataAccessProvider
{
    private readonly ILiteDatabase _database;

    public DataAccessProvider(Stream stream)
    {
        _database = new LiteDatabase(stream);
    }

    public Guid Add<T>(T entity)
    {
        return _database.GetCollection<T>().Insert(entity).AsGuid;
    }

    public T Get<T>(Guid id)
    {
        return _database.GetCollection<T>().FindById(id);
    }
    
    public IEnumerable<T>? GetAll<T>()
    {
        return _database.GetCollection<T>()
            .FindAll();
    }

    public int AddMany<T>(IEnumerable<T> collection)
    {
        return _database.GetCollection<T>().InsertBulk(collection);
    }

    public bool Update<T>(T entity)
    {
        return _database.GetCollection<T>().Update(entity);
    }

    public int UpdateMany<T>(Expression<Func<T, T>> action, Expression<Func<T, bool>> filter)
    {
        return _database.GetCollection<T>().UpdateMany(action, filter);
    }

    public bool Delete<T>(Guid id)
    {
        return _database.GetCollection<T>().Delete(id);
    }

    public int DeleteMany<T>(Expression<Func<T, bool>> filter)
    {
        return _database.GetCollection<T>().DeleteMany(filter);
    }

    public T? Find<T>(Expression<Func<T, bool>> filter)
    {
        return _database.GetCollection<T>().FindOne(filter);
    }

    public IEnumerable<T> FindMany<T>(
        Expression<Func<T, bool>>? filter,
        int skip = 0,
        int limit = Int32.MaxValue
    )
    {
        return _database.GetCollection<T>().Find(filter, skip, limit);
    }

}
