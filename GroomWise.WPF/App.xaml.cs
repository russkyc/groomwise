// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Threading;
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

        var scope = container.GetService<IAppServicesContainer>();
        scope?.AddContainer(container);

        var main = scope?.GetService<MainView>();
        var login = scope?.GetService<LoginView>();
        var navigation = scope?.GetService<INavigationService>();

        Dispatcher.BeginInvoke(() =>
        {
            navigation?.Initialize(SynchronizationContext.Current!, login!);
            navigation?.Add(AppViews.Login, login!);
            navigation?.Add(AppViews.Main, main!);
        });

        // Load Theme Defaults
        var config = scope?.GetService<IConfigurationService>();
        var themeManager = scope?.GetService<IThemeManagerService>();

        if (config is not null && themeManager is not null)
        {
            themeManager.SetDarkTheme(config.DarkMode);
            themeManager.SetColorTheme(config.ColorTheme);
        }

        MainWindow = login;
        MainWindow?.Show();
    }
}
