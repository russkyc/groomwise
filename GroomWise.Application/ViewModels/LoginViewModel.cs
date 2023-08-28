// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Application.Enums;
using GroomWise.Application.Observables;
using GroomWise.Domain.Enums;
using GroomWise.Infrastructure.Authentication.Enums;
using GroomWise.Infrastructure.Authentication.Interfaces;
using GroomWise.Infrastructure.Navigation.Interfaces;
using Injectio.Attributes;
using MvvmGen;
using Swordfish.NET.Collections;

namespace GroomWise.Application.ViewModels;

[ViewModel]
[ViewModelGenerateInterface]
[Inject(typeof(INavigationService))]
[Inject(typeof(IAuthenticationService))]
[RegisterSingleton]
public partial class LoginViewModel
{
    [Property]
    private string _username;

    [Property]
    private string _password;

    [Property]
    private bool _hasErrors;

    [Property]
    private ConcurrentObservableCollection<ObservableNotification> _notifications;

    partial void OnInitialize()
    {
        Notifications = new();
    }

    [Command]
    private async Task Login()
    {
        await Task.Run(async () =>
        {
            var result = AuthenticationService.Login(Username, Password);

            if (result is AuthenticationStatus.InvalidAccount)
            {
                HasErrors = true;
                Notifications.RemoveLast();
                Notifications.Add(
                    new ObservableNotification
                    {
                        Type = NotificationType.Danger,
                        Description = "Account is invalid."
                    }
                );
                return;
            }

            if (result is AuthenticationStatus.InvalidPassword)
            {
                HasErrors = true;
                Notifications.RemoveLast();
                Notifications.Add(
                    new ObservableNotification
                    {
                        Type = NotificationType.Danger,
                        Description = "Password is incorrect."
                    }
                );
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

            Username = string.Empty;
            Password = string.Empty;

            await Task.Delay(1000);
            Notifications.RemoveLast();
            NavigationService.Navigate(AppViews.Main);
        });
    }

    [Command]
    private async Task RemoveNotification(object param)
    {
        await Task.Run(() =>
        {
            if (param is ObservableNotification)
            {
                Notifications.RemoveLast();
            }
        });
    }
}
