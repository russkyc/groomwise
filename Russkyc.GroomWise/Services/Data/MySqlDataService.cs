// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Data;

public class MySqlDataService : IDatabaseService
{
    private IFreeSql _db;
    private IConfigurationService _configurationService;
    
    public MySqlDataService(IConfigurationService configurationService)
    {
        _configurationService = configurationService;
        Initialize();
    }

    void Initialize()
    {
        _db = new FreeSql.FreeSqlBuilder()
            .UseConnectionString(FreeSql.DataType.MySql,
                _configurationService.Config
                    .ReadString("ConnectionStrings",
                        "MySql"))
            .UseAutoSyncStructure(true)
            .Build();
    }
    
    public bool Add<T>(T item) where T : class, new()
    {
        _db.Insert(item);
        return _db.Select<T>()
            .Any(queryItem => queryItem == item);
    }

    public bool AddMultiple<T>(ICollection<T> items) where T : class, new()
    {
        _db.Insert(items);
        return true;
    }

    public T Get<T>(Expression<Func<T, bool>> filter) where T : class, new()
    {
        return _db.Select<T>()
            .Where(filter)
            .ToOne();
    }

    public ICollection<T> GetMultiple<T>(Expression<Func<T, bool>> filter) where T : class, new()
    {
        return _db.Select<T>()
            .Where(filter)
            .ToList();
    }

    public ICollection<T> GetCollection<T>() where T : class, new()
    {
        return _db.Select<T>()
            .ToList();
    }

    public bool Update<T>(Expression<Func<T, bool>> filter, Expression<Func<T, T>> action) where T : class, new()
    {
        return _db.Update<T>()
            .Set(action)
            .Where(filter)
            .ExecuteAffrows() != 0;
    }

    public bool Update<T>(Expression<Func<T, bool>> filter, T item) where T : class, new()
    {
        return _db.Update<T>()
            .Set(queryItem => item)
            .Where(filter)
            .ExecuteAffrows() != 0;
    }

    public bool Update<T>(T item) where T : class, new()
    {
        return _db.Update<T>()
            .Set(queryItem => item)
            .Where(queryItem => queryItem == item)
            .ExecuteAffrows() != 0;
    }

    public bool Delete<T>(Expression<Func<T, bool>> filter) where T : class, new()
    {
        return _db.Delete<T>()
            .Where(filter)
            .ExecuteDeleted()
            .Count > 0;
    }

    public bool Delete<T>(object id) where T : class, new()
    {
        throw new NotImplementedException();
    }

    public bool Contains<T>(Expression<Func<T, bool>> filter) where T : class, new()
    {
        return _db.Select<T>()
            .Where(filter)
            .Count() > 0;
    }
}