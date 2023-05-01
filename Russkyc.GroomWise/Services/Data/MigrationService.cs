// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Reflection;

namespace GroomWise.Services.Data;

public class MigrationService : IMigrationService
{
    private IConfigurationService _configurationService;
    private List<IDatabaseMigration> _databaseMigrations;

    public MigrationService(IConfigurationService configurationService)
    {
        _configurationService = configurationService;
        _databaseMigrations = new List<IDatabaseMigration>();
        foreach (Type type in Assembly
                     .GetExecutingAssembly()
                     .GetTypes()
                     .Where(type => type.Namespace == "GroomWise.Services.Data.Migrations"))
        {
            _databaseMigrations!.Add((IDatabaseMigration)Activator.CreateInstance(type)!);
        }
    }
    public void RunMigrations()
    {
        if (_configurationService.Config.ReadBoolean("AppSettings", "RunMigrations"))
        {
            _databaseMigrations.ForEach(migration => migration.Migrate());
            _configurationService.Config.WriteBoolean("AppSettings", "RunMigrations", false);
        }
    }
}