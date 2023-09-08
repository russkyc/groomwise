// GroomWise
// Copyright (C) 2023  John Russell C. Camo (@russkyc)
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY

using GroomWise.Application.Enums;
using GroomWise.Application.Events;
using GroomWise.Application.Observables;
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
    private ConcurrentObservableCollection<ObservableNotification> _notifications = new();

    partial void OnInitialize()
    {
        PageContext = DashboardViewModel;
    }

    [Command]
    private async Task Logout()
    {
        var dialogResult = await Task.Run(
            () =>
                DialogService.Create(
                    "GroomWise",
                    "Are you sure you want to log out?",
                    NavigationService
                )
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
    }

    [Command]
    private void NavigateToPage(object param)
    {
        if (param is Type type)
        {
            if (AppServicesContainer.GetService(type) is ViewModelBase viewModel)
            {
                PageContext = viewModel;
            }
        }
    }

    [Command]
    private void SetDarkTheme(object param)
    {
        if (param is bool useDarkTheme)
        {
            ConfigurationService.DarkMode = useDarkTheme;
            ThemeManagerService.SetDarkTheme(useDarkTheme);
            OnPropertyChanged(nameof(ConfigurationService.DarkMode));
        }
    }

    [Command]
    private void SetColorTheme(object param)
    {
        if (param is string themeId)
        {
            ConfigurationService.ColorTheme = themeId;
            ThemeManagerService.SetColorTheme(themeId);
            OnPropertyChanged(nameof(ConfigurationService.ColorTheme));
        }
    }

    [Command]
    private void RemoveNotification(object param)
    {
        if (param is ObservableNotification notification)
        {
            Notifications.Remove(notification);
        }
    }

    public void OnEvent(PublishNotificationEvent eventData)
    {
        var notification = new ObservableNotification
        {
            Title = eventData.Title,
            Description = eventData.Content,
            Type = eventData.NotificationType
        };
        Notifications.Add(notification);
        if (Notifications.Count > 8)
        {
            Notifications.RemoveAt(0);
        }
        Task.Run(async () =>
        {
            await Task.Delay(TimeSpan.FromSeconds(ConfigurationService.ToastCooldown));
            Notifications.Remove(notification);
        });
    }
}
