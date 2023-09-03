// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.ComponentModel;
using GroomWise.Infrastructure.Configuration.Interfaces;
using Injectio.Attributes;
using Russkyc.Configuration;

namespace GroomWise.Infrastructure.Configuration;

[RegisterSingleton<IConfigurationService, ConfigurationService>]
public class ConfigurationService : ConfigProvider, IConfigurationService
{
    public ConfigurationService()
        : base("appconfig.json") { }

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

    public double ToastCooldown
    {
        get => GetValue<double>(nameof(ToastCooldown));
        set => SetValue(nameof(ToastCooldown), value);
    }
}
