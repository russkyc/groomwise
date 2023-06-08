// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Abstractions;

public abstract class Repository<T> : Interfaces.Repository.IRepository<T>
    where T : class, IEquatable<T>, new()
{
    private readonly IDatabaseServiceAsync _databaseService;
    private readonly Queue<Task> _changes;
    private readonly List<T> _collection;

    public Repository(IDatabaseServiceAsync databaseService)
    {
        _changes = new Queue<Task>();
        _databaseService = databaseService;
        _collection = Task.Run(databaseService.GetCollection<T>).Result.ToList();
    }

    public void Add(T entity)
    {
        _collection.Add(entity);
        _changes.Enqueue(new Task(() => _databaseService.Add(entity)));
    }

    public void AddRange(IEnumerable<T> entities)
    {
        var collection = entities as T[] ?? entities.ToArray();
        _collection.AddRange(collection);
        _changes.Enqueue(new Task(() => _databaseService.AddMultiple(collection.ToList())));
    }

    public IEnumerable<T> GetAll()
    {
        return _collection;
    }

    public T? Find(Predicate<T> filter)
    {
        return _collection.Find(filter);
    }

    public IEnumerable<T> FindAll(Predicate<T> filter)
    {
        return _collection.FindAll(filter);
    }

    public bool Remove(T entity)
    {
        var result = _collection.Remove(entity);
        if (result)
            _changes.Enqueue(new Task(() => _databaseService.Delete<T>(t => entity == t)));
        return result;
    }

    public int RemoveAll(Predicate<T> filter)
    {
        var result = _collection.RemoveAll(filter);
        if (result > 0)
            _changes.Enqueue(new Task(() => _databaseService.Delete<T>(t => filter(t))));
        return result;
    }

    public void WriteToDb()
    {
        while (_changes.Count != 0)
        {
            _changes.Dequeue().Start();
        }
    }
}
