// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Collections;
using System.IO;
using JsonFlatFileDataStore;

namespace GroomWise.Services.Data;

public class MigrationService : IMigrationService
{
    private readonly ILogger _logger;
    private readonly IDatabaseServiceAsync _databaseServiceAsync;
    private readonly IConfigurationService _configurationService;

    public MigrationService(
        ILogger logger,
        IConfigurationService configurationService,
        IDatabaseServiceAsync databaseServiceAsync
    )
    {
        _logger = logger;
        _configurationService = configurationService;
        _databaseServiceAsync = databaseServiceAsync;
    }

    public void RunMigrations()
    {
        new DirectoryInfo(_configurationService.Config.ReadString("Database", "MigrationsPath"))
            .GetFiles()
            .ToList()
            .ForEach(file =>
            {
                var migration = new DataStore(file.FullName);
                var items = migration
                    .GetCollection(file.Name.ToLower().Replace(".json", ""))
                    .AsQueryable()
                    .ToList();
                _databaseServiceAsync.AddMultiple(items);
                _logger.Log(this, $"Run migrations from {file.Name}");
            });

        _configurationService.Config.WriteBoolean("Database", "RunMigrations", false);
        _logger.Log(this, "Done migrating");
    }
}
