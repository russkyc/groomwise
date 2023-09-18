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

using System.ComponentModel;
using GroomWise.Infrastructure.Configuration.Interfaces;
using Injectio.Attributes;
using Russkyc.Configuration;

namespace GroomWise.Infrastructure.Configuration;

[RegisterSingleton<IConfigurationService, ConfigurationService>]
public class ConfigurationService : ConfigProvider, IConfigurationService
{
    public ConfigurationService()
        : base("GroomWise.Settings.json") { }

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

    public bool MultiUser
    {
        get => GetValue<bool>(nameof(MultiUser));
        set => SetValue(nameof(MultiUser), value);
    }

    public bool CheckForUpdates
    {
        get => GetValue<bool>(nameof(CheckForUpdates));
        set => SetValue(nameof(CheckForUpdates), value);
    }

    public bool FirstRun
    {
        get => GetValue<bool>(nameof(FirstRun));
        set => SetValue(nameof(FirstRun), value);
    }
}
