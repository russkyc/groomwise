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
using System.Windows;
using GroomWise.Application.Enums;
using GroomWise.Application.Extensions;
using GroomWise.Infrastructure.Configuration.Interfaces;
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
        RegisterNavigationViews(scope);
        LoadThemeDefaults(scope);
        StartApp(scope);
    }

    private void RegisterNavigationViews(IAppServicesContainer scope)
    {
        Dispatcher.BeginInvoke(() =>
        {
            var navigation = scope.GetService<INavigationService>()!;
            navigation.Add(AppViews.Login, typeof(LoginView));
            navigation.Add(AppViews.Main, typeof(MainView));
        });
    }

    private void LoadThemeDefaults(IAppServicesContainer scope)
    {
        // Load Theme Defaults
        var config = scope.GetService<IConfigurationService>()!;
        var themeManager = scope.GetService<IThemeManagerService>()!;

        // Apply Theme Defaults
        themeManager.SetDarkTheme(config.DarkMode);
        themeManager.SetColorTheme(config.ColorTheme);
    }

    private void StartApp(IAppServicesContainer scope)
    {
        var configuration = scope.GetService<IConfigurationService>()!;
        var navigation = scope.GetService<INavigationService>()!;

        Current.Dispatcher.BeginInvoke(() =>
        {
            // Open Login if multi-user is enabled
            if (configuration.MultiUser)
            {
                var multiUserView = scope.GetService<LoginView>()!;
                navigation.Initialize(SynchronizationContext.Current!, multiUserView);
                MainWindow = multiUserView;
                MainWindow!.Show();
                return;
            }

            // Open Main Window if multi-user is disabled
            var singleUserView = scope.GetService<MainView>()!;
            navigation.Initialize(SynchronizationContext.Current!, singleUserView);
            MainWindow = singleUserView;
            MainWindow!.Show();
        });
    }
}
