﻿// GroomWise
// Copyright (C) 2023  John Russell C. Camo (@russkyc)
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GroomWise.Infrastructure.Navigation.Interfaces;
using GroomWise.Views.Dialogs;
using Injectio.Attributes;
using org.russkyc.moderncontrols;
using Swordfish.NET.Collections.Auxiliary;

namespace GroomWise.Services;

[RegisterSingleton<IDialogService, DialogService>]
public class DialogService : IDialogService
{
    public async Task<bool?> Create(
        string messageBoxText,
        string caption,
        INavigationService navigationService
    )
    {
        return await System.Windows.Application.Current.Dispatcher.InvokeAsync(() =>
        {
            if (
                System.Windows.Application.Current.Windows
                    .OfType<DialogView>()
                    .FirstOrDefault(
                        dialog =>
                            dialog.Owner == navigationService.CurrentWindow
                            && dialog.Caption!.Equals(caption)
                            && dialog.MessageBoxText!.Equals(messageBoxText)
                    ) is
                { }
            )
            {
                return false;
            }

            return new DialogView(messageBoxText, caption)
            {
                ShowInTaskbar = false,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = (Window)navigationService.CurrentWindow
            }.ShowDialog();
        });
    }

    public async Task<bool?> CreateOk(
        string messageBoxText,
        string caption,
        INavigationService navigationService
    )
    {
        return await System.Windows.Application.Current.Dispatcher.InvokeAsync(() =>
        {
            if (
                System.Windows.Application.Current.Windows
                    .OfType<DialogView>()
                    .FirstOrDefault(
                        dialog =>
                            dialog.Owner == navigationService.CurrentWindow
                            && dialog.Caption!.Equals(caption)
                            && dialog.MessageBoxText!.Equals(messageBoxText)
                    ) is
                { }
            )
            {
                return false;
            }

            return new DialogView(messageBoxText, caption, MessageBoxButton.OK)
            {
                ShowInTaskbar = false,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = (Window)navigationService.CurrentWindow
            }.ShowDialog();
        });
    }

    public async Task CloseDialogs(INavigationService navigationService)
    {
        await System.Windows.Application.Current.Dispatcher.InvokeAsync(() =>
        {
            if (
                System.Windows.Application.Current.Windows
                    .OfType<Window>()
                    .Where(dialog => dialog.Owner == navigationService.CurrentWindow)
                is not { } windows
            )
            {
                return;
            }

            if (navigationService.CurrentWindow is not Window owner)
            {
                return;
            }

            windows.ForEach(window =>
            {
                window.Close();
            });

            owner.Focus();
        });
    }

    public async Task CreateAddAccountDialog(
        object viewModel,
        INavigationService navigationService
    ) => await CreateNonReturningDialog<AddAccountView>(viewModel, navigationService);

    public async Task CreateAddAppointmentsDialog(
        object viewModel,
        INavigationService navigationService
    ) => await CreateNonReturningDialog<AddAppointmentsView>(viewModel, navigationService);

    public async Task CreateCustomerSelectionDialog(
        object viewModel,
        INavigationService navigationService
    ) => await CreateNonReturningDialog<SelectCustomerView>(viewModel, navigationService);

    public async Task CreateAddCustomersDialog(
        object viewModel,
        INavigationService navigationService
    ) => await CreateNonReturningDialog<AddCustomersView>(viewModel, navigationService);

    public async Task CreateEditCustomersDialog(
        object viewModel,
        INavigationService navigationService
    ) => await CreateNonReturningDialog<EditCustomersView>(viewModel, navigationService);

    public async Task CreateAddEmployeeDialog(
        object viewModel,
        INavigationService navigationService
    ) => await CreateNonReturningDialog<AddEmployeeView>(viewModel, navigationService);

    public async Task CreateEditEmployeeDialog(
        object viewModel,
        INavigationService navigationService
    ) => await CreateNonReturningDialog<AddCustomersView>(viewModel, navigationService);

    public async Task CreateAddServicesDialog(
        object viewModel,
        INavigationService navigationService
    ) => await CreateNonReturningDialog<AddServicesView>(viewModel, navigationService);

    public async Task CreateNonReturningDialog<T>(
        object viewModel,
        INavigationService navigationService
    ) =>
        await System.Windows.Application.Current.Dispatcher.InvokeAsync(() =>
        {
            if (System.Windows.Application.Current.Windows.OfType<T>().Any())
            {
                return;
            }

            if (Activator.CreateInstance<T>()! is not ModernWindow view)
            {
                return;
            }

            view.DataContext = viewModel;
            view.ShowInTaskbar = false;
            view.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            view.Owner = (ModernWindow)navigationService.CurrentWindow;
            view.Show();
        });
}
