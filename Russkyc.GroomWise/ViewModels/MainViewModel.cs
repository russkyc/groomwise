// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class MainViewModel : ViewModelBase, IMainViewModel
{
    [ObservableProperty]
    private ISessionManagerService _sessionManagerService;
    
    [ObservableProperty]
    private IThemeManagerService _themeManagerService;
    
    [ObservableProperty]
    private IApplicationService _applicationService;
    
    [ObservableProperty]
    private IPage? _view;

    public MainViewModel(
        ISessionManagerService sessionManagerService,
        IApplicationService applicationService,
        IThemeManagerService themeManagerService)
    {
        SessionManagerService = sessionManagerService;
        ThemeManagerService = themeManagerService;
        ApplicationService = applicationService;
    }

    [RelayCommand]
    private void SwitchBaseTheme(bool nightMode)
    {
        ThemeManagerService.UseDarkTheme(nightMode);
    }

    [RelayCommand]
    private void GetView(INavItem? navItem)
    {
        if (ApplicationService.NavItems?.Count > 0
            && navItem is { Selected: true })
        {
            View = BuilderServices.Resolve(navItem.Page) as IPage;
            ApplicationService.NavItems
                .First(item => item == navItem)
                .Selected = true;
        }
    }

    [RelayCommand]
    private void Logout()
    {
        if (MessageBox.Show("Are you sure you want to log out?",
                "GroomWise",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes) 
            SessionManagerService.EndSession();
    }
}