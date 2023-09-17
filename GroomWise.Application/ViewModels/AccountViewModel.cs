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

using GroomWise.Application.Events;
using GroomWise.Application.Extensions;
using GroomWise.Domain.Enums;
using GroomWise.Infrastructure.Authentication.Interfaces;
using GroomWise.Infrastructure.Database;
using GroomWise.Infrastructure.IoC.Interfaces;
using GroomWise.Infrastructure.Navigation.Interfaces;
using Injectio.Attributes;
using MvvmGen;
using MvvmGen.Events;
using Swordfish.NET.Collections;

namespace GroomWise.Application.ViewModels;

[ViewModel]
[ViewModelGenerateInterface]
[Inject(typeof(IAuthenticationService))]
[Inject(typeof(IDialogService))]
[Inject(typeof(INavigationService))]
[Inject(typeof(IEventAggregator))]
[Inject(typeof(IAppServicesContainer))]
[Inject(typeof(GroomWiseDbContext))]
[RegisterSingleton]
public partial class AccountViewModel
{
    [Property]
    private Role _role;

    [Property]
    private string _username;

    [Property]
    private string _password;

    [Property]
    private ConcurrentObservableCollection<Account> _accounts = new();

    partial void OnInitialize()
    {
        PopulateCollections();
    }

    void PopulateCollections()
    {
        Accounts = GroomWiseDbContext.Accounts.GetAll().AsObservableCollection();
    }

    [Command]
    async Task CreateAccount()
    {
        await DialogService.CreateAddAccountDialog(this, NavigationService);
    }

    [Command]
    private async Task SaveAccount()
    {
        var dialogResult = await Task.Run(
            () => DialogService.Create("Accounts", "Create Account?", NavigationService)
        );

        if (dialogResult is false)
        {
            return;
        }

        if (string.IsNullOrEmpty(Username))
        {
            EventAggregator.Publish(
                new PublishNotificationEvent(
                    "Saving Failed",
                    "Username cannot be empty.",
                    NotificationType.Danger
                )
            );
            return;
        }
        AuthenticationService.Register(Username, Password, Role);
        DialogService.CloseDialogs(NavigationService);
        EventAggregator.Publish(
            new PublishNotificationEvent(
                "Accounts Updated",
                $"Account {Username} added.",
                NotificationType.Success
            )
        );
        Username = string.Empty;
        Password = string.Empty;
        PopulateCollections();
    }
}
