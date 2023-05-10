// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Data;

public class SqliteDataServiceAsync : IDatabaseServiceAsync
{
    private readonly IConfigurationService _configurationService;
    private readonly ILogger _logger;

    private SQLiteAsyncConnection? _db;

    public SqliteDataServiceAsync(IConfigurationService configurationService, ILogger logger)
    {
        _configurationService = configurationService;
        _logger = logger;

        Initialize();
        CreateTables();
    }

    void Initialize()
    {
        _db = new SQLiteAsyncConnection(
            new SQLiteConnectionString(
                _configurationService.Config.ReadString("Database", "Path"),
                true,
                _configurationService.Config.ReadString("Database", "Password")
            )
        );
    }

    void CreateTables()
    {
        if (_configurationService.Config.ReadBoolean("Database", "RunMigrations"))
        {
            Task.Run(async () =>
            {
                await _db!.CreateTableAsync<Pet>()!;
                await _db!.CreateTableAsync<Account>()!;
                await _db!.CreateTableAsync<Address>()!;
                await _db!.CreateTableAsync<Employee>()!;
                await _db!.CreateTableAsync<Customer>()!;
                await _db!.CreateTableAsync<Appointment>()!;
            });
        }
    }

    public async Task<bool> Add<T>(T item)
        where T : class, new()
    {
        return await _db!.InsertAsync(item) > 0;
    }

    public async Task<bool> AddMultiple<T>(ICollection<T> items)
        where T : class, new()
    {
        return await _db!.InsertAllAsync(items) > 0;
    }

    public async Task<T> Get<T>(Expression<Func<T, bool>> filter)
        where T : class, new()
    {
        return await _db!.Table<T>().Where(filter).FirstOrDefaultAsync();
    }

    public async Task<ICollection<T>> GetMultiple<T>(Expression<Func<T, bool>> filter)
        where T : class, new()
    {
        return await _db!.Table<T>().Where(filter).ToListAsync();
    }

    public async Task<ICollection<T>> GetCollection<T>()
        where T : class, new()
    {
        return await _db!.Table<T>().ToListAsync();
    }

    public async Task<bool> Update<T>(
        Expression<Func<T, bool>> filter,
        Expression<Func<T, T>> action
    )
        where T : class, new()
    {
        return await _db!.UpdateAsync(action.Compile().Invoke(await _db?.GetAsync(filter)!)) > 0;
    }

    public async Task<bool> Update<T>(Expression<Func<T, bool>> filter, T item)
        where T : class, new()
    {
        return await _db!.UpdateAsync(item) > 0;
    }

    public async Task<bool> Update<T>(T item)
        where T : class, new()
    {
        return await _db!.UpdateAsync(item) > 0;
    }

    public async Task<bool> Delete<T>(Expression<Func<T, bool>> filter)
        where T : class, new()
    {
        return await _db!.DeleteAsync(filter) > 0;
    }

    public Task<bool> Delete<T>(object id)
        where T : class, new()
    {
        throw new NotImplementedException();
    }
}
