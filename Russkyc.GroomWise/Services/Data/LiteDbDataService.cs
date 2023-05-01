// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using LiteDB;

namespace GroomWise.Services.Data;

public class LiteDbDataService : IDatabaseService
{
    private IConfigurationService _configurationService;
    private ILiteDatabase _db;

    public LiteDbDataService(IConfigurationService configurationService)
    {
        _configurationService = configurationService;
        _db = new LiteDatabase(_configurationService.Config.ReadString("ConnectionStrings","LiteDb"));
    }

    public bool Add<T>(T item)
    {
        _db.GetCollection<T>().Insert(item);
        return _db.GetCollection<T>()
            .Query()
            .ToList()
            .Contains(item);
    }

    public bool AddMultiple<T>(ICollection<T> items)
    {
        var exists = true;
        _db.GetCollection<T>()
            .Insert(items);
        foreach (T item in items)
        {
            if (!_db.GetCollection<T>().Query()
                .ToList()
                .Contains(item))
            {
                exists = false;
            }
        }
        return exists;
    }

    public T Get<T>(Func<T, bool> filter)
    {
        return _db.GetCollection<T>()
            .Query()
            .ToList()
            .FirstOrDefault(filter)!;
    }

    public ICollection<T> GetMultiple<T>(Func<T, bool> filter)
    {
        return _db.GetCollection<T>()
            .Query()
            .ToList()
            .Where(filter)
            .ToList();
    }

    public ICollection<T> GetCollection<T>()
    {
        return _db.GetCollection<T>()
            .Query()
            .ToList();
    }

    public bool Update<T>(Func<T, bool> filter, Action<T> action)
    {
        _db.GetCollection<T>()
            .Query()
            .ToList()
            .Where(filter)
            .ToList()
            .ForEach(action);
        return Contains(filter);
    }

    public bool Update<T>(Func<T, bool> filter, T item)
    {
        throw new NotImplementedException();
    }

    public bool Delete<T>(Func<T, bool> filter)
    {
        throw new NotImplementedException();
    }

    public bool Contains<T>(Func<T, bool> filter)
    {
        return _db.GetCollection<T>().Exists(x => filter(x));
    }
}