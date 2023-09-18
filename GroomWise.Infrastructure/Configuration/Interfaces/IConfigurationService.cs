// GroomWise
// Copyright (C) 2023  John Russell C. Camo (@russkyc)
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY

namespace GroomWise.Infrastructure.Configuration.Interfaces;

public interface IConfigurationService
{
    bool DarkMode { get; set; }
    string ColorTheme { get; set; }
    string Version { get; set; }
    string AppDataPath { get; set; }
    double ToastCooldown { get; set; }
    bool MultiUser { get; set; }
    bool CheckForUpdates { get; set; }
    bool FirstRun { get; set; }
}
