// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Threading;
using GroomWise.Application.Enums;
using GroomWise.Infrastructure.IoC.Interfaces;
using GroomWise.Infrastructure.Navigation.Interfaces;
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

        var services = new ServiceCollection()
            .AddGroomWiseInfrastructure()
            .AddGroomWiseApplication()
            .AddGroomWise();

        var container = services.BuildServiceProvider();
        var scope = container.GetService<IAppServicesContainer>();
        scope?.AddContainer(container);

        var main = scope?.GetService<MainView>();
        var login = scope?.GetService<LoginView>();
        var navigation = scope?.GetService<INavigationService>();

        Current.MainWindow = login;

        Current.Dispatcher.BeginInvoke(() =>
        {
            navigation?.Initialize(SynchronizationContext.Current!, login!);
            navigation?.Add(AppViews.Login, login!);
            navigation?.Add(AppViews.Main, main!);
        });
        MainWindow?.Show();
    }

    /*protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Set the maximum amount of memory that the application can use to 2 GB for x86 and 4 GB for x64.
        IntPtr maxWorkingSet = new IntPtr(
            (IntPtr.Size == 4) ? (int)(1.5 * 1024 * 1024 * 1024) : 4L * 1024L * 1024L * 1024L
        );

        Process.GetCurrentProcess().MaxWorkingSet = maxWorkingSet;
    }*/
}
