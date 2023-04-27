// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System;
using org.russkyc.moderncontrols.Helpers;
using Russkyc.GroomWise.Desktop.ViewModels;
using Russkyc.GroomWise.Desktop.Views;

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
        ThemeManager.Instance.SetBaseTheme("Light");
        
        var view = new MainView
        {
            DataContext = Activator.CreateInstance<MainViewModel>()
        };

        view.Show();
    }
}