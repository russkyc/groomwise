// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Abstractions;

public abstract class Repository<T> : Interfaces.Repository.IRepository<T>
    where T : class, new()
{
    private List<T> _collection;

    public Repository(IDatabaseServiceAsync databaseService)
    {
        _collection = Task.Run(databaseService.GetCollection<T>).Result.ToList();
    }

    public void Add(T entity)
    {
        _collection.Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        _collection.AddRange(entities);
    }

    public IEnumerable<T> GetAll()
    {
        return _collection;
    }

    public T? Find(Predicate<T> filter)
    {
        return _collection.Find(filter);
    }

    public IEnumerable<T>? FindAll(Predicate<T> filter)
    {
        return _collection.FindAll(filter);
    }

    public bool Remove(T entity)
    {
        return _collection.Remove(entity);
    }

    public int RemoveAll(Predicate<T> filter)
    {
        return _collection.RemoveAll(filter);
    }
}
