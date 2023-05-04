// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

#region

using LiteDB;

#endregion

namespace GroomWise.Services.Data;

public class LiteDbDataService : IDatabaseService
{
    private readonly IConfigurationService _configurationService;
    private readonly ILiteDatabase _db;

    public LiteDbDataService(IConfigurationService configurationService)
    {
        _configurationService = configurationService;
        _db = new LiteDatabase(
            _configurationService.Config.ReadString("ConnectionStrings", "LiteDb") + _configurationService.Key);
    }

    public bool Add<T>(T item) where T : new()
    {
        _db.GetCollection<T>().Insert(item);
        return _db.GetCollection<T>()
            .Query()
            .ToList()
            .Contains(item);
    }

    public bool AddMultiple<T>(ICollection<T> items) where T : new()
    {
        var exists = true;
        _db.GetCollection<T>()
            .Insert(items);
        foreach (T item in items)
            if (!_db.GetCollection<T>().Query()
                    .ToList()
                    .Contains(item))
                exists = false;
        return exists;
    }

    public T Get<T>(Func<T, bool> filter) where T : new()
    {
        return _db.GetCollection<T>()
            .Query()
            .ToList()
            .FirstOrDefault(filter)!;
    }

    public ICollection<T> GetMultiple<T>(Func<T, bool> filter) where T : new()
    {
        return _db.GetCollection<T>()
            .Query()
            .ToList()
            .Where(filter)
            .ToList();
    }

    public ICollection<T> GetCollection<T>() where T : new()
    {
        return _db.GetCollection<T>()
            .Query()
            .ToList();
    }

    public bool Update<T>(Func<T, bool> filter, Func<T, T> action) where T : new()
    {
        return _db.GetCollection<T>()
            .UpdateMany(t => action(t), t => filter(t)) > 0;
    }

    public bool Update<T>(Func<T, bool> filter, T item) where T : new()
    {
        throw new NotImplementedException();
    }

    public bool Update<T>(T item) where T : new()
    {
        throw new NotImplementedException();
    }

    public bool Delete<T>(Func<T, bool> filter) where T : new()
    {
        return _db.GetCollection<T>()
            .DeleteMany(t => filter(t)) > 0;
    }

    public bool Delete<T>(object id) where T : new()
    {
        return _db.GetCollection<T>()
            .Delete(id as BsonValue);
    }

    public bool Contains<T>(Func<T, bool> filter)
    {
        return _db.GetCollection<T>().Exists(x => filter(x));
    }
}