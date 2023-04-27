// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GroomWise.Core.Interfaces;
using GroomWise.Core.Models;
using Russkyc.Services.Services;

namespace Russkyc.GroomWise.Desktop.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private IAppInstance _appInstance;

    [ObservableProperty]
    private IView? _view;
    
    public MainViewModel(IAppInstance appInstance)
    {
        _appInstance = appInstance;
        Init();
    }

    void Init()
    {
        AppInstance.NavItems.AddRange(new []
        {
            new NavItem
            {
                Name = "Dashboard",
                Page = "DashboardView"
            },
            new NavItem
            {
                Name = "Appointments",
                Page = "AppointmentsView"
            },
            new NavItem
            {
                Name = "Services",
                Page = "ServicesView"
            },
            new NavItem
            {
                Name = "Customers",
                Page = "CustomersView"
            },
            new NavItem
            {
                Name = "Pets",
                Page = "PetsView"
            },
            new NavItem
            {
                Name = "Reports",
                Page = "ReportsView"
            }
        });
    }

    [RelayCommand]
    private void GetView(string pageName)
    {
        View = Injector.GetInstance()
            .Resolve<IView>(pageName);
    }
}