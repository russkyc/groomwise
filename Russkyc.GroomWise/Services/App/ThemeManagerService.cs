// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.App;

public class ThemeManagerService : IThemeManagerService
{
    private readonly IConfigurationService _configurationService;

    public ThemeManagerService(IConfigurationService configurationService)
    {
        _configurationService = configurationService;
        ThemeManager.Instance.AddColorTheme(
            "Default", "pack://application:,,,/GroomWise;component/Views/Resources/Themes/ColorThemes/Default.xaml");
        UseDarkTheme(_configurationService.Config.ReadBoolean("AppSettings", "DarkMode"));
        UseColorTheme(_configurationService.Config.ReadString("AppSettings", "ColorTheme"));
    }

    public bool DarkMode { get; set; }

    public void UseDarkTheme(bool night)
    {
        SaveBaseTheme(night);
        ThemeManager.Instance.SetBaseTheme(night ? "Dark" : "Light");
        DarkMode = night;
    }

    public void UseColorTheme(string color)
    {
        SaveColorTheme(color);
        ThemeManager.Instance.SetColorTheme(color);
    }

    public void Reset()
    {
        SaveBaseTheme(false);
        UseDarkTheme(false);
        SaveColorTheme("Default");
        UseColorTheme("Default");
    }

    private void SaveColorTheme(string color)
    {
        _configurationService.Config
            .WriteString("AppSettings", "ColorTheme", color);

    }

    private void SaveBaseTheme(bool dark)
    {
        _configurationService.Config
            .WriteBoolean("AppSettings", "DarkMode", dark);
    }
}