// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Data;

public class MigrationService : IMigrationService
{
    private readonly ILogger _logger;
    private readonly IConfigurationService _configurationService;
    private readonly List<IDatabaseMigration> _databaseMigrations;

    public MigrationService(ILogger logger, IConfigurationService configurationService)
    {
        _logger = logger;
        _configurationService = configurationService;
        _databaseMigrations = new List<IDatabaseMigration>();
        GetMigrations();
    }

    void GetMigrations()
    {
        Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.Namespace == "GroomWise.Services.Data.Migrations")
            .ToList()
            .ForEach(type =>
            {
                _logger.Log(this, $"Reading migrations from {type.FullName}");
                _databaseMigrations.Add((IDatabaseMigration)Activator.CreateInstance(type)!);
            });
    }

    public void RunMigrations()
    {
        if (_configurationService.Config.ReadBoolean("Database", "RunMigrations"))
        {
            _databaseMigrations.ForEach(migration =>
            {
                migration.Migrate();
                _logger.Log(this, $"Run migrations for {migration.GetType()}");
            });
            _configurationService.Config.WriteBoolean("Database", "RunMigrations", false);
        }
        else
        {
            _logger.Log(this, "Run Migrations: False");
        }
    }
}
