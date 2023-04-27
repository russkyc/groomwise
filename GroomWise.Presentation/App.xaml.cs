// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Application.Common.Models;
using GroomWise.Core.Interfaces;
using org.russkyc.moderncontrols;
using Russkyc.GroomWise.Desktop.Factories;
using Russkyc.GroomWise.Desktop.Views;
using Russkyc.GroomWise.Desktop.Views.Pages;
using Russkyc.Services.Entities;
using Russkyc.Services.Enums;
using Russkyc.Services.Services;

namespace Russkyc.GroomWise.Desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    /// <summary>
    /// Application Entry for GroomWise.Presentation
    /// </summary>
    public App()
    {
        InitializeComponent();

        Injector.GetInstance()
            .Use(new DependencyContainer());
        
        Injector.GetInstance()
            .Register<IView,MainView>("MainView", Scope.Shared);
        Injector.GetInstance()
            .Register<IView,DashboardView>("DashboardView", Scope.Shared);
        Injector.GetInstance()
            .Register<IView,AppointmentsView>("AppointmentsView", Scope.Shared);
        Injector.GetInstance()
            .Register<IView,ServicesView>("ServicesView", Scope.Shared);
        Injector.GetInstance()
            .Register<IView,CustomersView>("CustomersView", Scope.Shared);
        Injector.GetInstance()
            .Register<IView,PetsView>("PetsView", Scope.Shared);
        Injector.GetInstance()
            .Register<IView,ReportsView>("ReportsView", Scope.Shared);

        var view = Injector.GetInstance().Resolve<IView>("MainView") as ModernWindow;
        view.DataContext = ViewModelFactory.CreateMainViewModel(new AppInstance());
        view.Show();
    }
}