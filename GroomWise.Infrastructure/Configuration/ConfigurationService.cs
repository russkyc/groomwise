// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Infrastructure.Configuration.Interfaces;
using Russkyc.Configuration;

namespace GroomWise.Infrastructure.Configuration;

public class ConfigurationService : ConfigProvider, IConfigurationService
{
    public ConfigurationService(string path)
        : base(path) { }

    public bool DarkMode
    {
        get => GetValue<bool>(nameof(DarkMode));
        set => SetValue(nameof(DarkMode), value);
    }

    public string ColorTheme
    {
        get => GetValue<string>(nameof(ColorTheme));
        set => SetValue(nameof(ColorTheme), value);
    }

    public string Version
    {
        get => GetValue<string>(nameof(Version));
        set => SetValue(nameof(Version), value);
    }

    public string AppDataPath
    {
        get => GetValue<string>(nameof(AppDataPath));
        set => SetValue(nameof(AppDataPath), value);
    }
}
