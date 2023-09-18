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

namespace GroomWise.Infrastructure.Navigation.Interfaces;

public interface IDialogService
{
    Task<bool?> CreateYesNo(
        string messageBoxText,
        string caption,
        INavigationService navigationService
    );

    Task<bool?> CreateOkCancel(
        string messageBoxText,
        string caption,
        INavigationService navigationService
    );

    Task<bool?> CreateOk(
        string messageBoxText,
        string caption,
        INavigationService navigationService
    );

    Task CloseDialogs(INavigationService navigationService);

    Task CreateAddAccountDialog(
        object viewModel,
        INavigationService navigationService
    );

    Task CreateAddAppointmentsDialog(
        object viewModel,
        INavigationService navigationService
    );

    Task CreateCustomerSelectionDialog(
        object viewModel,
        INavigationService navigationService
    );

    Task CreateAddCustomersDialog(
        object viewModel,
        INavigationService navigationService
    );

    Task CreateEditCustomersDialog(
        object viewModel,
        INavigationService navigationService
    );

    Task CreateAddEmployeeDialog(
        object viewModel,
        INavigationService navigationService
    );

    Task CreateEditEmployeeDialog(
        object viewModel,
        INavigationService navigationService
    );

    Task CreateAddServicesDialog(
        object viewModel,
        INavigationService navigationService
    );
}
