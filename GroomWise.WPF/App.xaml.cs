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

using System.Threading;
using GroomWise.Application.Enums;
using GroomWise.Application.Extensions;
using GroomWise.Infrastructure.Configuration.Interfaces;
using GroomWise.Infrastructure.Database;
using GroomWise.Infrastructure.IoC.Interfaces;
using GroomWise.Infrastructure.Navigation.Interfaces;
using GroomWise.Infrastructure.Theming.Interfaces;
using GroomWise.Views;
using GroomWise.Views.Dialogs;
using Microsoft.Extensions.DependencyInjection;

namespace GroomWise;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    /// <summary>
    /// Application Entry for GroomWise.Desktop
    /// </summary>
    public App()
    {
        InitializeComponent();

        var container = new ServiceCollection()
            .AddGroomWiseInfrastructure()
            .AddGroomWiseApplication()
            .AddGroomWise()
            .AddEventAggregator()
            .BuildServiceProvider();

        var scope = container.GetService<IAppServicesContainer>()!;
        scope.AddContainer(container);

#if DEBUG
        // ResetDatabase(scope);
#endif
        RegisterNavigationViews(scope);
        LoadThemeDefaults(scope);
        StartApp(scope);
    }

    private void RegisterNavigationViews(IAppServicesContainer scope)
    {
        Dispatcher.BeginInvoke(() =>
        {
            var navigation = scope.GetService<INavigationService>()!;

            var main = scope.GetService<MainView>();
            var login = scope.GetService<LoginView>();
            var addAppointment = scope.GetService<AddAppointmentsView>();

            navigation.Initialize(SynchronizationContext.Current!, login!);
            navigation.Add(AppViews.Login, login!);
            navigation.Add(AppViews.Main, main!);
            navigation.Add(AppViews.AddAppointment, addAppointment!);
        });
    }

    private void LoadThemeDefaults(IAppServicesContainer scope)
    {
        // Load Theme Defaults
        var config = scope.GetService<IConfigurationService>();
        var themeManager = scope.GetService<IThemeManagerService>();

        if (config is not null && themeManager is not null)
        {
            themeManager.SetDarkTheme(config.DarkMode);
            themeManager.SetColorTheme(config.ColorTheme);
        }
    }

    private void ResetDatabase(IAppServicesContainer scope)
    {
        var context = scope.GetService<GroomWiseDbContext>();
        context?.Appointments.DeleteMultiple(t => true);
        context?.Customers.DeleteMultiple(t => true);
        context?.Employees.DeleteMultiple(t => true);
        context?.Products.DeleteMultiple(t => true);
        context?.Roles.DeleteMultiple(t => true);
        context?.GroomingServices.DeleteMultiple(t => true);
        context?.Pets.DeleteMultiple(t => true);
    }

    private void StartApp(IAppServicesContainer scope)
    {
        MainWindow = scope.GetService<LoginView>();
        MainWindow?.Show();
    }
}
