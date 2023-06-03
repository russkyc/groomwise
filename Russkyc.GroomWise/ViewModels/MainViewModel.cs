// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class MainViewModel : ViewModelBase, IMainViewModel
{
    [ObservableProperty]
    private IDialogFactory _dialogFactory;

    [ObservableProperty]
    private IHotkeyListenerService _hotkeyListenerService;

    [ObservableProperty]
    private ISessionManagerService _sessionManagerService;

    [ObservableProperty]
    private IThemeManagerService _themeManagerService;

    [ObservableProperty]
    private IApplicationService _applicationService;

    [ObservableProperty]
    private NavItemsCollection _navItems;

    [ObservableProperty]
    private IPage? _view;

    public MainViewModel(
        ISessionManagerService sessionManagerService,
        IApplicationService applicationService,
        IThemeManagerService themeManagerService,
        IDialogFactory dialogFactory,
        IHotkeyListenerService hotkeyListenerService
    )
    {
        _dialogFactory = dialogFactory;
        _hotkeyListenerService = hotkeyListenerService;
        SessionManagerService = sessionManagerService;
        ThemeManagerService = themeManagerService;
        ApplicationService = applicationService;
        NavItems = ApplicationService.NavItems!;
    }

    [RelayCommand]
    private void SwitchBaseTheme(bool nightMode)
    {
        ThemeManagerService.UseDarkTheme(nightMode);
    }

    [RelayCommand]
    private async void GetView(INavItem? navItem)
    {
        if (ApplicationService.NavItems?.Count > 0 && navItem is { Selected: true })
        {
            await Task.Run(async () =>
            {
                await DispatchHelper.UiInvokeAsync(() =>
                {
                    View = BuilderServices.Resolve(navItem.Page) as IPage;
                    ApplicationService.NavItems.First(item => item == navItem).Selected = true;
                });
            });
        }
    }

    [RelayCommand]
    private async void OpenSettings()
    {
        await Task.Run(async () =>
        {
            await DispatchHelper.UiInvokeAsync(
                () => View = BuilderServices.Resolve<ISettingsView>()
            );
        });
    }

    [RelayCommand]
    private void Logout()
    {
        if (
            DialogFactory
                .Create(dialog =>
                {
                    dialog.MessageBoxText = "Are you sure?";
                    dialog.Caption = "Do you want to log out? You can log back in anytime.";
                })
                .ShowDialog() == true
        )
        {
            HotkeyListenerService.UnregisterAll();
            SessionManagerService.EndSession();
            BuilderServices.Resolve<ILoginView>().ClearFields("Password");
            BuilderServices.Resolve<ILoginView>().Show();
            BuilderServices.Resolve<IMainView>().Hide();
        }
    }
}
