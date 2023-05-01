// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class MainViewModel : ViewModelBase, IMainViewModel
{
    private IThemeManagerService _themeManagerService;
    
    [ObservableProperty]
    private IAppService _appService;

    [ObservableProperty]
    private INavItem _selectedPage;

    [ObservableProperty]
    private IView? _view;

    [ObservableProperty]
    private bool _nightMode;
    
    public MainViewModel(
        IAppService appService,
        IThemeManagerService themeManagerService)
    {
        AppService = appService;
        _themeManagerService = themeManagerService;
        NightMode = _themeManagerService.DarkMode;
        SelectedPage = AppService.NavItems.First(item => item.Selected);
    }

    [RelayCommand]
    private void SwitchBaseTheme()
    {
        _themeManagerService.UseDarkTheme(NightMode);
    }

    [RelayCommand]
    private void GetView(INavItem navItem)
    {
        View = BuilderServices.Resolve(navItem.Page) as IView;
        AppService!.NavItems.First(item => item == navItem).Selected = true;
    }
}