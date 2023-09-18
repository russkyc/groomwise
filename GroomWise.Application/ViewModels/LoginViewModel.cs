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
using GroomWise.Infrastructure.Navigation.Interfaces;
using Injectio.Attributes;
using MvvmGen;
using MvvmGen.Events;
using Swordfish.NET.Collections;

namespace GroomWise.Application.ViewModels;

[ViewModel]
[ViewModelGenerateInterface]
[Inject(typeof(IEventAggregator))]
[Inject(typeof(INavigationService))]
[Inject(typeof(IAuthenticationService))]
[Inject(typeof(IConfigurationService), PropertyAccessModifier = AccessModifier.Public)]
[RegisterSingleton]
public partial class LoginViewModel : IEventSubscriber<LogoutEvent>
{
    [Property]
    private string _username;

    [Property]
    private string _password;

    [Property]
    private bool _hasErrors;

    [Property]
    private ConcurrentObservableCollection<ObservableNotification> _notifications = new();

    [Command]
    private async Task Login()
    {
        if (string.IsNullOrEmpty(Username))
        {
            HasErrors = true;
            Notifications.RemoveLast();
            Notifications.Add(
                new ObservableNotification
                {
                    Type = NotificationType.Danger,
                    Description = "Username cannot be blank."
                }
            );
            return;
        }

        if (string.IsNullOrEmpty(Password))
        {
            HasErrors = true;
            Notifications.RemoveLast();
            Notifications.Add(
                new ObservableNotification
                {
                    Type = NotificationType.Danger,
                    Description = "Password cannot be blank."
                }
            );
            return;
        }

        var result = AuthenticationService.Login(Username, Password);
        if (
            result is AuthenticationStatus.InvalidAccount
            || result is AuthenticationStatus.InvalidPassword
        )
        {
            HasErrors = true;
            Notifications.RemoveLast();
            Notifications.Add(
                new ObservableNotification
                {
                    Type = NotificationType.Danger,
                    Description = "Username or Password is incorrect."
                }
            );
            EventAggregator.Publish(new LoginEvent(AuthenticationStatus.InvalidPassword));
            return;
        }

        HasErrors = false;
        Notifications.RemoveLast();
        Notifications.Add(
            new ObservableNotification
            {
                Type = NotificationType.Success,
                Description = "Login successful."
            }
        );

        EventAggregator.Publish(new LoginEvent(AuthenticationStatus.Authenticated));

        Password = string.Empty;
        Username = string.Empty;

        await Task.Delay(300);
        Notifications.RemoveLast();
        NavigationService.Navigate(AppViews.Main);
    }

    [Command]
    async Task CreateAdminAccount()
    {
        if (string.IsNullOrEmpty(Username))
        {
            HasErrors = true;
            Notifications.RemoveLast();
            Notifications.Add(
                new ObservableNotification
                {
                    Type = NotificationType.Danger,
                    Description = "Username cannot be blank."
                }
            );
            return;
        }

        if (string.IsNullOrEmpty(Password))
        {
            HasErrors = true;
            Notifications.RemoveLast();
            Notifications.Add(
                new ObservableNotification
                {
                    Type = NotificationType.Danger,
                    Description = "Password cannot be blank."
                }
            );
            return;
        }

        AuthenticationService.Register(Username, Password, Role.Admin);
        ConfigurationService.FirstRun = false;
        HasErrors = false;
        Notifications.RemoveLast();
        Notifications.Add(
            new ObservableNotification
            {
                Type = NotificationType.Success,
                Description = "Admin account created."
            }
        );

        Password = string.Empty;
        Username = string.Empty;

        await Task.Delay(300);
        Notifications.RemoveLast();
        NavigationService.Navigate(AppViews.Login);
    }

    [Command]
    private void RemoveNotification(object param)
    {
        Notifications.RemoveLast();
    }

    public void OnEvent(LogoutEvent eventData)
    {
        Task.Run(async () =>
        {
            await Task.Delay(200);
            Notifications.RemoveLast();
            Notifications.Add(
                new ObservableNotification
                {
                    Type = NotificationType.Danger,
                    Description = "Session Ended."
                }
            );
        });
    }
}
