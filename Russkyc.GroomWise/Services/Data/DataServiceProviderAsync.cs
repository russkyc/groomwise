﻿// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Data;

public class DataServiceProviderAsync : IDatabaseServiceAsync
{
    private readonly IFreeSql _db;

    public DataServiceProviderAsync(IConfigurationService configurationService)
    {
#if (DEBUG)
        _db = new FreeSqlBuilder()
            .UseConnectionString(
                FreeSql.DataType.Sqlite,
                new ConnectionSourceProvider().Build(
                    new ConnectionSource().WithPath(
                        configurationService.Config.ReadString("Database", "Path")
                    ),
                    DbProvider.Sqlite
                )
            )
#else
        _db = new FreeSqlBuilder()
            .UseConnectionString(
                FreeSql.DataType.MySql,
                new ConnectionSourceProvider().Build(
                    new ConnectionSource()
                        .WithPath(configurationService.Config.ReadString("Database", "Path"))
                        .WithDatabase(
                            configurationService.Config.ReadString("Database", "Database")
                        )
                        .WithUsername(
                            configurationService.Config.ReadString("Database", "Username")
                        )
                        .WithPassword(
                            configurationService.Config.ReadString("Database", "Password")
                        ),
                    DbProvider.MySql
                )
            )
# endif
            .UseAutoSyncStructure(true)
            .UseLazyLoading(true)
            .Build();
    }

    public async Task<bool> Add<T>(T item)
        where T : class, new()
    {
        return await _db.Insert(item).ExecuteIdentityAsync() > 0;
    }

    public async Task<bool> AddMultiple<T>(ICollection<T> items)
        where T : class, new()
    {
        return await _db.Insert(items.ToList()).ExecuteIdentityAsync() > 0;
    }

    public async Task<T> Get<T>(Expression<Func<T, bool>> filter)
        where T : class, new()
    {
        return await _db.Select<T>().Where(filter).ToOneAsync();
    }

    public async Task<ICollection<T>> GetMultiple<T>(Expression<Func<T, bool>> filter)
        where T : class, new()
    {
        return await _db.Select<T>().Where(filter).ToListAsync();
    }

    public async Task<ICollection<T>> GetCollection<T>()
        where T : class, new()
    {
        return await _db.Select<T>().ToListAsync();
    }

    public async Task<bool> Update<T>(
        Expression<Func<T, bool>> filter,
        Expression<Func<T, T>> action
    )
        where T : class, new()
    {
        return await _db.Update<T>().Set(t => action).Where(filter).ExecuteAffrowsAsync() > 0;
    }

    public Task<bool> Update<T>(Expression<Func<T, bool>> filter, T item)
        where T : class, new()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update<T>(T item)
        where T : class, new()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Delete<T>(Expression<Func<T, bool>> filter)
        where T : class, new()
    {
        return await _db.Delete<T>().Where(filter).ExecuteAffrowsAsync() > 0;
    }

    public Task<bool> Delete<T>(object id)
        where T : class, new()
    {
        throw new NotImplementedException();
    }
}