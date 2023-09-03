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

using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GroomWise.Infrastructure.Navigation.Interfaces;
using GroomWise.Views.Dialogs;
using Injectio.Attributes;
using Swordfish.NET.Collections.Auxiliary;

namespace GroomWise.Services;

[RegisterSingleton<IDialogService, DialogService>]
public class DialogService : IDialogService
{
    public bool? Create(string messageBoxText, string caption, INavigationService navigationService)
    {
        return Task.Run(async () =>
        {
            return await App.Current.Dispatcher.InvokeAsync(() =>
            {
                if (
                    App.Current.Windows
                        .OfType<DialogView>()
                        .FirstOrDefault(
                            dialog =>
                                dialog.Owner == navigationService.CurrentWindow
                                && dialog.Caption.Equals(caption)
                                && dialog.MessageBoxText.Equals(messageBoxText)
                        ) is
                    { } window
                )
                {
                    return false;
                }

                return new DialogView(messageBoxText, caption)
                {
                    ShowInTaskbar = false,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = (Window)navigationService.CurrentWindow!
                }.ShowDialog();
            });
        }).Result;
    }

    public bool? CreateOk(
        string messageBoxText,
        string caption,
        INavigationService navigationService
    )
    {
        return Task.Run(async () =>
        {
            return await App.Current.Dispatcher.InvokeAsync(() =>
            {
                if (
                    App.Current.Windows
                        .OfType<DialogView>()
                        .FirstOrDefault(
                            dialog =>
                                dialog.Owner == navigationService.CurrentWindow
                                && dialog.Caption.Equals(caption)
                                && dialog.MessageBoxText.Equals(messageBoxText)
                        ) is
                    { } window
                )
                {
                    return false;
                }

                return new DialogView(messageBoxText, caption, MessageBoxButton.OK)
                {
                    ShowInTaskbar = false,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = (Window)navigationService.CurrentWindow!
                }.ShowDialog();
            });
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
                        .Where(dialog => dialog.Owner == navigationService.CurrentWindow) is
                    { } windows
                )
                {
                    var owner = navigationService.CurrentWindow as Window;
                    windows.ForEach(window =>
                    {
                        window.Close();
                    });
                    owner!.Focus();
                }
            });
        });
    }

    public void CreateCustomerSelectionDialog(
        object viewModel,
        INavigationService navigationService
    )
    {
        Task.Run(async () =>
        {
            await App.Current.Dispatcher.InvokeAsync(() =>
            {
                if (!App.Current.Windows.OfType<SelectCustomerView>().Any())
                {
                    new SelectCustomerView(viewModel)
                    {
                        ShowInTaskbar = false,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner,
                        Owner = (Window)navigationService.CurrentWindow!
                    }.Show();
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

    public void CreateEditCustomersDialog(object viewModel, INavigationService navigationService)
    {
        Task.Run(async () =>
        {
            await App.Current.Dispatcher.InvokeAsync(() =>
            {
                if (!App.Current.Windows.OfType<EditCustomersView>().Any())
                {
                    new EditCustomersView(viewModel)
                    {
                        ShowInTaskbar = false,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner,
                        Owner = (Window)navigationService.CurrentWindow!
                    }.Show();
                }
            });
        });
    }

    public void CreateAddServicesDialog(object viewModel, INavigationService navigationService)
    {
        Task.Run(async () =>
        {
            await App.Current.Dispatcher.InvokeAsync(() =>
            {
                if (!App.Current.Windows.OfType<AddServicesView>().Any())
                {
                    new AddServicesView(viewModel)
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
