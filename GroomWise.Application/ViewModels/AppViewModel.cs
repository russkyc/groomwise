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
using GroomWise.Domain.Enums;
using GroomWise.Infrastructure.Authentication.Enums;
using GroomWise.Infrastructure.Authentication.Interfaces;
using GroomWise.Infrastructure.Configuration.Interfaces;
using GroomWise.Infrastructure.IoC.Interfaces;
using GroomWise.Infrastructure.Navigation.Interfaces;
using GroomWise.Infrastructure.Scheduler.Interfaces;
using GroomWise.Infrastructure.Theming.Interfaces;
using GroomWise.Infrastructure.Updater.Interfaces;
using Injectio.Attributes;
using MvvmGen;
using MvvmGen.Events;
using MvvmGen.ViewModels;
using Swordfish.NET.Collections;

namespace GroomWise.Application.ViewModels;

[Inject(typeof(IAuthenticationService))]
[Inject(typeof(IDialogService))]
[Inject(typeof(IUpdater))]
[Inject(typeof(INavigationService))]
[Inject(typeof(IAppServicesContainer))]
[Inject(typeof(IThemeManagerService))]
[Inject(typeof(IEventAggregator))]
[Inject(typeof(IScheduler))]
[Inject(typeof(IConfigurationService), PropertyAccessModifier = AccessModifier.Public)]
[Inject(typeof(DashboardViewModel))]
[ViewModel]
[RegisterSingleton]
public partial class AppViewModel
    : IEventSubscriber<PublishNotificationEvent>,
        IEventSubscriber<LoginEvent>
{
    [Property]
    private Role _sessionRole;

    [Property]
    private ViewModelBase _pageContext;

    [Property]
    private double _progress;

    [Property]
    private ConcurrentObservableCollection<ObservableNotification> _notifications = new();

    partial void OnInitialize()
    {
        PageContext = DashboardViewModel;
    }

    [Command]
    public async Task CheckForUpdates(object param)
    {
        try
        {
            if (param is not bool silent)
            {
                return;
            }

            var canUpdate = await Updater.CheckForUpdates(silent);

            if (canUpdate is false)
            {
                return;
            }

            var progress = new Progress<double>(val => Progress = val * 100);
            await Updater.PerformUpdate(progress);
        }
        catch (HttpRequestException)
        {
            if (param is not bool silent)
            {
                return;
            }

            if (silent)
            {
                return;
            }

            await Task.Run(
                () =>
                    DialogService.CreateOk(
                        "No Network Connection",
                        $"Cannot check for updates.",
                        NavigationService
                    )
            );
        }
    }

    [Command]
    private async Task Logout()
    {
        var dialogResult = await Task.Run(
            () =>
                DialogService.CreateYesNo(
                    "GroomWise",
                    "Are you sure you want to log out?",
                    NavigationService
                )
        );

        if (dialogResult is false)
        {
            return;
        }

        if (AuthenticationService.Logout() is AuthenticationStatus.NotAuthenticated)
        {
            await Task.Delay(300);
            await DialogService.CloseDialogs(NavigationService);
            NavigationService.Navigate(AppViews.Login);
            EventAggregator.Publish(new LogoutEvent());
        }
    }

    [Command]
    private void NavigateToPage(object param)
    {
        if (param is not Type type)
        {
            return;
        }
        if (AppServicesContainer.GetService(type) is not ViewModelBase viewModel)
        {
            return;
        }

        PageContext = viewModel;
    }

    [Command]
    private void SetDarkTheme(object param)
    {
        if (param is not bool useDarkTheme)
        {
            return;
        }
        ConfigurationService.DarkMode = useDarkTheme;
        ThemeManagerService.SetDarkTheme(useDarkTheme);
        OnPropertyChanged(nameof(ConfigurationService.DarkMode));
    }

    [Command]
    private void SetColorTheme(object param)
    {
        if (param is not string themeId)
        {
            return;
        }
        ConfigurationService.ColorTheme = themeId;
        ThemeManagerService.SetColorTheme(themeId);
        OnPropertyChanged(nameof(ConfigurationService.ColorTheme));
    }

    [Command]
    private void RemoveNotification(object param)
    {
        if (param is not ObservableNotification notification)
        {
            return;
        }
        Notifications.Remove(notification);
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

    public void OnEvent(LoginEvent eventData)
    {
        if (eventData.Status is AuthenticationStatus.NotAuthenticated)
        {
            return;
        }

        var session = AuthenticationService.GetSession();

        if (session is null)
        {
            return;
        }

        if (eventData.Status is AuthenticationStatus.Authenticated)
        {
            PageContext = AppServicesContainer.GetService<DashboardViewModel>()!;
        }

        SessionRole = session.Role;
    }
}
