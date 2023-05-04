// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Data;

public class LiteDbDataService : IDatabaseService
{
    private readonly IConfigurationService _configurationService;
    private readonly ILiteDatabaseFactory _liteDatabaseFactory;
    private readonly ILogger _logger;
    private ILiteDatabase? _db;

    public LiteDbDataService(
        ILogger logger,
        IConfigurationService configurationService,
        ILiteDatabaseFactory liteDatabaseFactory)
    {
        _logger = logger;
        _configurationService = configurationService;
        _liteDatabaseFactory = liteDatabaseFactory;

        Initialize();
    }


    void Initialize()
    {
        _db = _liteDatabaseFactory.Create(
            _configurationService.Config
                .ReadString("ConnectionStrings", "LiteDb") + _configurationService.Key);
        _logger.Log(this, _db is null ? "Failed to create database file" : "Created database file");
    }
    public bool Add<T>(T item) where T : new()
    {
        _db!.GetCollection<T>().Insert(item);
        _logger.Log(this, $"Added {typeof(T)} to Database");
        return _db.GetCollection<T>()
            .Query()
            .ToList()
            .Contains(item);
    }

    public bool AddMultiple<T>(ICollection<T> items) where T : new()
    {
        _db!.GetCollection<T>()
            .Insert(items);
        _logger.Log(this, $"Added {typeof(T)} collection to Database");
        return items.Any(
            item => _db.GetCollection<T>()
                .Query()
                .ToList()
                .Contains(item));
    }

    public T Get<T>(Func<T, bool> filter) where T : new()
    {
        _logger.Log(this, $"Finding {typeof(T)} from Database");
        return _db!.GetCollection<T>()
            .Query()
            .ToList()
            .FirstOrDefault(filter)!;
    }

    public ICollection<T> GetMultiple<T>(Func<T, bool> filter) where T : new()
    {
        _logger.Log(this, $"Finding Multiple {typeof(T)} from Database");
        return _db!.GetCollection<T>()
            .Query()
            .ToList()
            .Where(filter)
            .ToList();
    }

    public ICollection<T> GetCollection<T>() where T : new()
    {
        _logger.Log(this, $"Get {typeof(T)} Collection from Database");
        return _db!.GetCollection<T>()
            .Query()
            .ToList();
    }

    public bool Update<T>(Func<T, bool> filter, Func<T, T> action) where T : new()
    {
        _logger.Log(this, $"Update Multiple {typeof(T)} from Database");
        return _db!.GetCollection<T>()
            .UpdateMany(item => action(item),
                t => filter(t)) > 0;
    }

    public bool Update<T>(Func<T, bool> filter, T item) where T : new()
    {
        _logger.Log(this, $"Update Multiple {typeof(T)} from Database");
        return _db!.GetCollection<T>()
            .UpdateMany(t => item,
                t => filter(t)) > 0;
    }

    public bool Update<T>(T item) where T : new()
    {
        throw new NotImplementedException();
    }

    public bool Delete<T>(Func<T, bool> filter) where T : new()
    {
        _logger.Log(this, $"Delete Multiple {typeof(T)} from Database");
        return _db!.GetCollection<T>()
            .DeleteMany(t => filter(t)) > 0;
    }

    public bool Delete<T>(object id) where T : new()
    {
        _logger.Log(this, $"Delete Multiple {typeof(T)} where id = {id} from Database");
        return _db!.GetCollection<T>()
            .Delete(id as BsonValue);
    }

    public bool Contains<T>(Func<T, bool> filter)
    {
        _logger.Log(this, $"Count {filter.Method.GetParameters()[0].GetType()} Collection from Database");
        return _db!.GetCollection<T>()
            .Exists(item => filter(item));
    }
}