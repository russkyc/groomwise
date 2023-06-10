// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels.App;

public partial class MainViewModel : ViewModelBase, IMainViewModel
{
    private readonly IUnitOfWork _dbContext;
    private readonly IConfigProvider _configProvider;
    private readonly IApplicationService _applicationService;

    [ObservableProperty]
    private IFactory<DialogView> _dialogFactory;

    [ObservableProperty]
    private ISessionManagerService _sessionManagerService;

    [ObservableProperty]
    private IThemeManagerService _themeManagerService;

    [ObservableProperty]
    private SynchronizedObservableCollection<NavItem> _navItems;

    [ObservableProperty]
    private IPage? _view;

    public MainViewModel(
        IFactory<DialogView> dialogFactory,
        IUnitOfWork dbContext,
        IConfigProvider configProvider,
        IApplicationService applicationService,
        IThemeManagerService themeManagerService,
        ISessionManagerService sessionManagerService
    )
    {
        _dbContext = dbContext;
        _dialogFactory = dialogFactory;
        _configProvider = configProvider;
        _applicationService = applicationService;

        ThemeManagerService = themeManagerService;
        SessionManagerService = sessionManagerService;

        NavItems = _applicationService.NavItems!;
    }

    [RelayCommand]
    private void SwitchBaseTheme(bool nightMode)
    {
        ThemeManagerService.UseDarkTheme(nightMode);
    }

    [RelayCommand]
    private async void GetView(NavItem? navItem)
    {
        if (_applicationService.NavItems?.Count == 0 || navItem is { Selected: false })
            return;
        await DispatchHelper.UiInvokeAsync(() =>
        {
            View = BuilderServices.Resolve(navItem.Page) as IPage;
            _applicationService.NavItems.First(item => item == navItem).Selected = true;
        });
    }

    [RelayCommand]
    private async void OpenSettings()
    {
        await DispatchHelper.UiInvokeAsync(() => View = BuilderServices.Resolve<ISettingsView>());
    }

    [RelayCommand]
    private async void Logout()
    {
        await DispatchHelper.UiInvokeAsync(() =>
        {
            if (
                DialogFactory
                    .Create(dialog =>
                    {
                        dialog.MessageBoxText = "Are you sure?";
                        dialog.Caption = "Do you want to log out? You can log back in anytime.";
                    })
                    .ShowDialog() == false
            )
                return;

            // Write changes to database
            _dbContext.SaveChanges();

            SessionManagerService.EndSession();
            BuilderServices.Resolve<ILoginView>().ClearFields("Username", "Password");
            BuilderServices.Resolve<ILoginView>().Show();
            BuilderServices.Resolve<IMainView>().Hide();
        });
    }
}
