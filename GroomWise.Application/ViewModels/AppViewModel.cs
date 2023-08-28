// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Application.Enums;
using GroomWise.Domain.Interfaces;
using GroomWise.Infrastructure.Authentication.Enums;
using GroomWise.Infrastructure.Authentication.Interfaces;
using GroomWise.Infrastructure.Navigation;
using MvvmGen;

namespace GroomWise.Application.ViewModels;

[Inject(typeof(IAuthenticationService))]
[Inject(typeof(IDialogFactory))]
[ViewModel]
[ViewModelGenerateInterface]
public partial class AppViewModel
{
    [Command]
    private async Task Logout()
    {
        await Task.Run(async () =>
        {
            var dialogResult = DialogFactory.Create(
                "GroomWise",
                "Are you sure you want to log out?"
            );

            if (dialogResult == true)
            {
                var result = AuthenticationService.Logout();

                await Task.Delay(500);
                if (result.Equals(AuthenticationStatus.NotAuthenticated))
                {
                    NavigationService.Instance?.Navigate(NavigationPage.Login);
                }
            }
        });
    }
}
