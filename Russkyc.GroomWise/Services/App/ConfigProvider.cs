// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.App;

public class ConfigProvider : IConfigProvider
{
    // Ini Config File
    private IniFile _settings => new IniFile("appconfig.ini");

    // App Settings
    public bool DarkMode
    {
        get => _settings.ReadBoolean("AppSettings", nameof(DarkMode).ToCamelCase());
        set => _settings.WriteBoolean("AppSettings", nameof(DarkMode).ToCamelCase(), value);
    }

    public string ColorTheme
    {
        get => _settings.ReadString("AppSettings", nameof(ColorTheme).ToCamelCase());
        set => _settings.WriteString("AppSettings", nameof(ColorTheme).ToCamelCase(), value);
    }

    public string Version
    {
        get => _settings.ReadString("AppSettings", nameof(Version).ToCamelCase());
        set => _settings.WriteString("AppSettings", nameof(Version).ToCamelCase(), value);
    }

    // Database Settings
    public string Path
    {
        get => _settings.ReadString("DatabaseSettings", nameof(Path).ToCamelCase());
        set => _settings.WriteString("DatabaseSettings", nameof(Path).ToCamelCase(), value);
    }
    public string Database
    {
        get => _settings.ReadString("DatabaseSettings", nameof(Database).ToCamelCase());
        set => _settings.WriteString("DatabaseSettings", nameof(Database).ToCamelCase(), value);
    }
    public string Username
    {
        get => _settings.ReadString("DatabaseSettings", nameof(Username).ToCamelCase());
        set => _settings.WriteString("DatabaseSettings", nameof(Username).ToCamelCase(), value);
    }
    public string Password
    {
        get => _settings.ReadString("DatabaseSettings", nameof(Password).ToCamelCase());
        set => _settings.WriteString("DatabaseSettings", nameof(Password).ToCamelCase(), value);
    }

    // Migrations
    public string MigrationsPath
    {
        get => _settings.ReadString("MigrationSettings", nameof(MigrationsPath).ToCamelCase());
        set =>
            _settings.WriteString("MigrationSettings", nameof(MigrationsPath).ToCamelCase(), value);
    }
    public bool RunMigrations
    {
        get => _settings.ReadBoolean("MigrationSettings", nameof(RunMigrations).ToCamelCase());
        set =>
            _settings.WriteBoolean("MigrationSettings", nameof(RunMigrations).ToCamelCase(), value);
    }

    // Debugging
    public string LogPath
    {
        get => _settings.ReadString("DebugSettings", nameof(LogPath).ToCamelCase());
        set => _settings.WriteString("DebugSettings", nameof(LogPath).ToCamelCase(), value);
    }
    public bool Logging
    {
        get => _settings.ReadBoolean("DebugSettings", nameof(Logging).ToCamelCase());
        set => _settings.WriteBoolean("DebugSettings", nameof(Logging).ToCamelCase(), value);
    }
}
