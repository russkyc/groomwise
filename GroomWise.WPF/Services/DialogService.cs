// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GroomWise.Infrastructure.Navigation.Interfaces;
using GroomWise.Views.Dialogs;
using Injectio.Attributes;

namespace GroomWise.Services;

[RegisterSingleton<IDialogService, DialogService>]
public class DialogService : IDialogService
{
    public bool? Create(string messageBoxText, string caption, INavigationService navigationService)
    {
        return Task.Run(async () =>
        {
            return await App.Current.Dispatcher.InvokeAsync(
                () =>
                    new DialogView(messageBoxText, caption)
                    {
                        ShowInTaskbar = false,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner,
                        Owner = (Window)navigationService.CurrentWindow!
                    }.ShowDialog()
            );
        }).Result;
    }

    public void CloseDialogs(INavigationService navigationService)
    {
        Task.Run(async () =>
        {
            await App.Current.Dispatcher.InvokeAsync(() =>
            {
                if (
                    App.Current.Windows
                        .OfType<Window>()
                        .FirstOrDefault(
                            dialog => dialog.Owner == navigationService.CurrentWindow
                        ) is
                    { } window
                )
                {
                    var owner = navigationService.CurrentWindow as Window;
                    window.Close();
                    owner!.Focus();
                }
            });
        });
    }

    public void CreateAddAppointmentsDialog(object viewModel, INavigationService navigationService)
    {
        Task.Run(async () =>
        {
            await App.Current.Dispatcher.InvokeAsync(() =>
            {
                if (!App.Current.Windows.OfType<AddAppointmentsView>().Any())
                {
                    new AddAppointmentsView(viewModel)
                    {
                        ShowInTaskbar = false,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner,
                        Owner = (Window)navigationService.CurrentWindow!
                    }.Show();
                }
            });
        });
    }

    public void CreateAddCustomersDialog(object viewModel, INavigationService navigationService)
    {
        Task.Run(async () =>
        {
            await App.Current.Dispatcher.InvokeAsync(() =>
            {
                if (!App.Current.Windows.OfType<AddCustomersView>().Any())
                {
                    new AddCustomersView(viewModel)
                    {
                        ShowInTaskbar = false,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner,
                        Owner = (Window)navigationService.CurrentWindow!
                    }.Show();
                }
            });
        });
    }
}
