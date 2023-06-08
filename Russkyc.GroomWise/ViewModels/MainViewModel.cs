// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class MainViewModel : ViewModelBase, IMainViewModel
{
    private readonly IContextManager _contextManager;

    [ObservableProperty]
    private DialogFactory _dialogFactory;

    [ObservableProperty]
    private ISessionManagerService _sessionManagerService;

    [ObservableProperty]
    private IThemeManagerService _themeManagerService;

    [ObservableProperty]
    private IApplicationService _applicationService;

    [ObservableProperty]
    private SynchronizedObservableCollection<NavItem> _navItems;

    [ObservableProperty]
    private IPage? _view;

    public MainViewModel(
        DialogFactory dialogFactory,
        IContextManager contextManager,
        IApplicationService applicationService,
        IThemeManagerService themeManagerService,
        ISessionManagerService sessionManagerService
    )
    {
        _dialogFactory = dialogFactory;
        _contextManager = contextManager;

        ApplicationService = applicationService;
        ThemeManagerService = themeManagerService;
        SessionManagerService = sessionManagerService;

        NavItems = ApplicationService.NavItems!;
    }

    [RelayCommand]
    private void SwitchBaseTheme(bool nightMode)
    {
        ThemeManagerService.UseDarkTheme(nightMode);
    }

    [RelayCommand]
    private async void GetView(NavItem? navItem)
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
            // Write changes to database
            _contextManager.WriteChanges();

            SessionManagerService.EndSession();
            BuilderServices.Resolve<ILoginView>().ClearFields("Username", "Password");
            BuilderServices.Resolve<ILoginView>().Show();
            BuilderServices.Resolve<IMainView>().Hide();
        }
    }
}
