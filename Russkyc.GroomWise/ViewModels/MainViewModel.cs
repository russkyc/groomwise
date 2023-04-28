// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Linq;

namespace Russkyc.GroomWise.ViewModels;

public partial class MainViewModel : ViewModelBase, IMainViewModel
{
    private IThemeManagerService _themeManagerService;
    
    [ObservableProperty]
    private IAppService _appService;

    [ObservableProperty]
    private INavItem _selectedPage;

    [ObservableProperty]
    private IView? _view;
    
    public MainViewModel(
        IAppService appService,
        IThemeManagerService themeManagerService)
    {
        _themeManagerService = themeManagerService;
        AppService = appService;
        SelectedPage = AppService.NavItems.First(item => item.Selected);
    }

    [RelayCommand]
    private void SwitchBaseTheme(bool night)
    {
        _themeManagerService.UseNightBaseTheme(night);
    }

    [RelayCommand]
    private void GetView(INavItem navItem)
    {
        AppService!.NavItems.First(item => item == navItem).Selected = true;
        View = BuilderServices.Resolve(navItem.Page) as IView;
    }
}