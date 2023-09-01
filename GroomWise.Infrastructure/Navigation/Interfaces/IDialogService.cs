// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Infrastructure.Navigation.Interfaces;

public interface IDialogService
{
    bool? Create(string messageBoxText, string caption, INavigationService navigationService);
    void CloseDialogs(INavigationService navigationService);

    void CreateCustomerSelectionDialog(
        object viewModel,
        INavigationService navigationService
    );

    void CreateAddAppointmentsDialog(object viewModel, INavigationService navigationService);
    void CreateAddCustomersDialog(object viewModel, INavigationService navigationService);
    void CreateEditCustomersDialog(object viewModel, INavigationService navigationService);
    void CreateAddServicesDialog(object viewModel, INavigationService navigationService);
}
