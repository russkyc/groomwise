// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Infrastructure.Configuration.Interfaces;

public interface IConfigurationService
{
    bool DarkMode { get; set; }
    string ColorTheme { get; set; }
    string Version { get; set; }
    string AppDataPath { get; set; }
    double ToastCooldown { get; set; }
}
