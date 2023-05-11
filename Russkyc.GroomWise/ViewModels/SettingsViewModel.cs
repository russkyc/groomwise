// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class SettingsViewModel : ObservableObject, ISettingsViewModel
{
    private readonly IThemeManagerService _themeManagerService;

    public SettingsViewModel(IThemeManagerService themeManagerService)
    {
        _themeManagerService = themeManagerService;
    }

    [RelayCommand]
    private void SwitchColorTheme(string theme)
    {
        _themeManagerService.UseColorTheme(theme);
    }
}
