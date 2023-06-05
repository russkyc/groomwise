// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Interfaces;

public interface IConfigProvider
{
    bool DarkMode { get; set; }
    string ColorTheme { get; set; }
    string Version { get; set; }
    string Path { get; set; }
    string Database { get; set; }
    string Username { get; set; }
    string Password { get; set; }
    string MigrationsPath { get; set; }
    bool RunMigrations { get; set; }
    string LogPath { get; set; }
    bool Logging { get; set; }
}
