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
    bool? Create(string messageBoxText, string caption, INavigationService navigationService);

    bool? CreateOk(
        string messageBoxText,
        string caption,
        INavigationService navigationService
    );

    void CloseDialogs(INavigationService navigationService);

    void CreateCustomerSelectionDialog(
        object viewModel,
        INavigationService navigationService
    );

    void CreateAddAppointmentsDialog(object viewModel, INavigationService navigationService);
    void CreateAddCustomersDialog(object viewModel, INavigationService navigationService);
    void CreateAddEmployeeDialog(object viewModel, INavigationService navigationService);
    void CreateEditCustomersDialog(object viewModel, INavigationService navigationService);
    void CreateAddServicesDialog(object viewModel, INavigationService navigationService);
}
