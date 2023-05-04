// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class MainViewModel : ViewModelBase, IMainViewModel
{
    [ObservableProperty] private IThemeManagerService _themeManagerService;
    [ObservableProperty] private IApplicationService _applicationService;
    [ObservableProperty] private ISessionService _sessionService;
    [ObservableProperty] private INavItem? _selectedPage;
    [ObservableProperty] private IView? _view;

    public MainViewModel(
        ISessionService sessionService,
        IApplicationService applicationService,
        IThemeManagerService themeManagerService)
    {
        ThemeManagerService = themeManagerService;
        ApplicationService = applicationService;
        SessionService = sessionService;
    }

    [RelayCommand]
    private void SwitchBaseTheme(bool nightMode)
    {
        ThemeManagerService?.UseDarkTheme(nightMode);
    }

    [RelayCommand]
    private void GetView(INavItem? navItem)
    {
        if (ApplicationService.NavItems.Count > 0 && navItem != null && (bool)navItem.Selected)
        {
            View = BuilderServices.Resolve(navItem.Page) as IView;
            ApplicationService!.NavItems.First(item => item == navItem).Selected = true;
        }
    }

    [RelayCommand]
    private void Logout()
    {
        if (MessageBox.Show("Are you sure you want to log out?", "GroomWise", MessageBoxButton.YesNo) ==
            MessageBoxResult.Yes)
        {
            SessionService.Logout();
        }
    }
}