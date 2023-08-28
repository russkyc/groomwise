// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Application.Enums;
using GroomWise.Infrastructure.Authentication.Enums;
using GroomWise.Infrastructure.Authentication.Interfaces;
using GroomWise.Infrastructure.Navigation.Interfaces;
using Injectio.Attributes;
using MvvmGen;
using MvvmGen.ViewModels;

namespace GroomWise.Application.ViewModels;

[Inject(typeof(IAuthenticationService))]
[Inject(typeof(IDialogFactory))]
[Inject(typeof(INavigationService))]
[Inject(typeof(DashboardViewModel))]
[ViewModel]
[ViewModelGenerateInterface]
[RegisterSingleton]
public partial class AppViewModel
{
    [Property]
    private ViewModelBase _pageContext;

    partial void OnInitialize()
    {
        PageContext = DashboardViewModel;
    }

    [Command]
    private async Task Logout()
    {
        await Task.Run(async () =>
        {
            var dialogResult = DialogFactory.Create(
                "GroomWise",
                "Are you sure you want to log out?",
                NavigationService
            );

            if (dialogResult == true)
            {
                var result = AuthenticationService.Logout();

                await Task.Delay(500);
                if (result.Equals(AuthenticationStatus.NotAuthenticated))
                {
                    NavigationService.Navigate(AppViews.Login);
                }
            }
        });
    }
}
