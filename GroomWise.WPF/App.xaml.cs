// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using GroomWise.Application.ViewModels;
using GroomWise.Containers;
using GroomWise.Views.Dialogs;
using Russkyc.DependencyInjection.Helpers;
using Russkyc.DependencyInjection.Implementations;

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
        /*var container = new ServicesCollection()
            .AddServicesFromAssembly(Assembly.Load("GroomWise.Domain"))
            .AddServicesFromAssembly(Assembly.Load("GroomWise.Infrastructure"))
            .AddServicesFromAssembly(Assembly.Load("GroomWise.Application"))
            .AddServices()
            .Build();*/
        var container = new ServiceProvider();
        Current.MainWindow = container.GetService<LoginView>();
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
