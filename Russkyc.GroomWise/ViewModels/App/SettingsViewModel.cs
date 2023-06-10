// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels.App;

public partial class SettingsViewModel : ViewModelBase, ISettingsViewModel
{
    [ObservableProperty]
    private IThemeManagerService _themeManagerService;

    [ObservableProperty]
    private ObservableCollection<string> _baseThemes;

    [ObservableProperty]
    private ObservableCollection<string> _colorThemes;

    public SettingsViewModel(IThemeManagerService themeManagerService)
    {
        ThemeManagerService = themeManagerService;
        BaseThemes = new ObservableCollection<string>();
        ColorThemes = new ObservableCollection<string>();
        GetThemeItems();
    }

    [RelayCommand]
    private void SwitchColorTheme(string theme)
    {
        ThemeManagerService.UseColorTheme(theme);
    }

    [RelayCommand]
    private void SwitchBaseTheme(bool isDark)
    {
        ThemeManagerService.UseDarkTheme(isDark);
    }

    private void GetThemeItems()
    {
        ThemeManagerService.GetBaseThemes().ToList().ForEach(BaseThemes.Add);
        ThemeManagerService.GetColorThemes().ToList().ForEach(ColorThemes.Add);
    }
}
