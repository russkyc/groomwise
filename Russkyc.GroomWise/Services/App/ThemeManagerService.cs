// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.App;

public partial class ThemeManagerService : ObservableObject, IThemeManagerService
{
    private readonly ILogger _logger;
    private readonly IConfigurationService _configurationService;
    
    [ObservableProperty]
    private bool _darkMode;
    
    public ThemeManagerService(
        ILogger logger,
        IConfigurationService configurationService)
    {
        _logger = logger;
        _configurationService = configurationService;
        ThemeManager.Instance.AddColorTheme(
            "Default", "pack://application:,,,/GroomWise;component/Views/Resources/Themes/ColorThemes/Default.xaml");
        UseDarkTheme(_configurationService.Config.ReadBoolean("AppSettings", "DarkMode"));
        UseColorTheme(_configurationService.Config.ReadString("AppSettings", "ColorTheme"));
    }

    public void UseDarkTheme(bool night)
    {
        SaveBaseTheme(night);
        ThemeManager.Instance.SetBaseTheme(night ? "Dark" : "Light");
        _logger.Log(this, $"Set dark mode {night}");
        DarkMode = night;
    }

    public void UseColorTheme(string color)
    {
        SaveColorTheme(color);
        ThemeManager.Instance.SetColorTheme(color);
        _logger.Log(this, $"Set color theme {color}");
    }

    public void Reset()
    {
        SaveBaseTheme(false);
        UseDarkTheme(false);
        SaveColorTheme("Default");
        UseColorTheme("Default");
        _logger.Log(this,"Reset color theme");
    }

    private void SaveColorTheme(string color)
    {
        _configurationService.Config
            .WriteString("AppSettings", "ColorTheme", color);
        _logger.Log(this,$"Save color theme {color}");

    }

    private void SaveBaseTheme(bool dark)
    {
        _configurationService.Config
            .WriteBoolean("AppSettings", "DarkMode", dark);
        _logger.Log(this,$"Save base dark theme {dark}");
    }
}