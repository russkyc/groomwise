// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Application.Enums;
using GroomWise.Application.Events;
using GroomWise.Application.Observables;
using GroomWise.Domain.Enums;
using GroomWise.Infrastructure.Authentication.Enums;
using GroomWise.Infrastructure.Authentication.Interfaces;
using GroomWise.Infrastructure.Configuration.Interfaces;
using GroomWise.Infrastructure.IoC.Interfaces;
using GroomWise.Infrastructure.Navigation.Interfaces;
using GroomWise.Infrastructure.Theming.Interfaces;
using Injectio.Attributes;
using MvvmGen;
using MvvmGen.Events;
using MvvmGen.ViewModels;
using Swordfish.NET.Collections;

namespace GroomWise.Application.ViewModels;

[Inject(typeof(IAuthenticationService))]
[Inject(typeof(IDialogService))]
[Inject(typeof(INavigationService))]
[Inject(typeof(IAppServicesContainer))]
[Inject(typeof(IThemeManagerService))]
[Inject(typeof(IEventAggregator))]
[Inject(typeof(IConfigurationService), PropertyAccessModifier = AccessModifier.Public)]
[Inject(typeof(DashboardViewModel))]
[ViewModel]
[RegisterSingleton]
public partial class AppViewModel : IEventSubscriber<PublishNotificationEvent>
{
    [Property]
    private ViewModelBase _pageContext;

    [Property]
    private IList<Role> _authenticatedUserRoles;

    [Property]
    private ConcurrentObservableCollection<ObservableNotification> _notifications;

    partial void OnInitialize()
    {
        Notifications = new();
        PageContext = DashboardViewModel;
        AuthenticatedUserRoles = AuthenticationService.GetSession()?.Roles!;
    }

    [Command]
    private async Task Logout()
    {
        await Task.Run(async () =>
        {
            var dialogResult = DialogService.Create(
                "GroomWise",
                "Are you sure you want to log out?",
                NavigationService
            );

            if (dialogResult == true)
            {
                var result = AuthenticationService.Logout();

                await Task.Delay(300);
                if (result.Equals(AuthenticationStatus.NotAuthenticated))
                {
                    DialogService.CloseDialogs(NavigationService);
                    NavigationService.Navigate(AppViews.Login);
                    EventAggregator.Publish(new LogoutEvent());
                }
            }
        });
    }

    [Command]
    private async Task NavigateToPage(object param)
    {
        await Task.Run(() =>
        {
            if (param is Type type)
            {
                if (AppServicesContainer.GetService(type) is ViewModelBase viewModel)
                {
                    PageContext = viewModel;
                }
            }
        });
    }

    [Command]
    private async Task SetDarkTheme(object param)
    {
        await Task.Run(() =>
        {
            if (param is bool useDarkTheme)
            {
                ConfigurationService.DarkMode = useDarkTheme;
                ThemeManagerService.SetDarkTheme(useDarkTheme);
                OnPropertyChanged(nameof(ConfigurationService.DarkMode));
            }
        });
    }

    [Command]
    private async Task SetColorTheme(object param)
    {
        await Task.Run(() =>
        {
            if (param is string themeId)
            {
                ConfigurationService.ColorTheme = themeId;
                ThemeManagerService.SetColorTheme(themeId);
                OnPropertyChanged(nameof(ConfigurationService.ColorTheme));
            }
        });
    }

    [Command]
    private async Task RemoveNotification(object param)
    {
        if (param is ObservableNotification notification)
        {
            Notifications.Remove(notification);
        }
    }

    public void OnEvent(PublishNotificationEvent eventData)
    {
        Notifications.Add(
            new ObservableNotification
            {
                Description = eventData.Content,
                Type = eventData.NotificationType
            }
        );
        if (Notifications.Count > 3)
        {
            Notifications.RemoveAt(0);
        }
    }
}
