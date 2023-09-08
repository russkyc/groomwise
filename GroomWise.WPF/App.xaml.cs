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
using System.Threading.Tasks;
using System.Windows;
using GroomWise.Application.Enums;
using GroomWise.Application.Extensions;
using GroomWise.Infrastructure.Configuration.Interfaces;
using GroomWise.Infrastructure.IoC.Interfaces;
using GroomWise.Infrastructure.Navigation.Interfaces;
using GroomWise.Infrastructure.Theming.Interfaces;
using GroomWise.Views;
using GroomWise.Views.Dialogs;
using Hangfire;
using Hangfire.LiteDB;
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

    private void StartApp(IAppServicesContainer scope)
    {
        GlobalConfiguration.Configuration.UseLiteDbStorage();
        var configuration = scope.GetService<IConfigurationService>();
        var navigation = scope.GetService<INavigationService>()!;

        if (configuration is null)
        {
            return;
        }

        Current.Dispatcher.BeginInvoke(() =>
        {
            IWindow window;
            if (configuration.MultiUser)
            {
                window = scope.GetService<LoginView>()!;
            }
            else
            {
                window = scope.GetService<MainView>()!;
            }

            navigation.Initialize(SynchronizationContext.Current!, window);
            MainWindow = window as Window;
            MainWindow!.Show();
        });
    }
}
