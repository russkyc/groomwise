// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System;
using GroomWise.Application.ViewModels;
using GroomWise.Infrastructure.Navigation.Interfaces;
using Injectio.Attributes;
using org.russkyc.moderncontrols;

namespace GroomWise.Views;

[RegisterSingleton]
public partial class MainView : ModernWindow, IWindow
{
    public MainView(AppViewModel vm)
    {
        DataContext = vm;
        InitializeComponent();
    }

    protected override void OnClosed(EventArgs e)
    {
        System.Windows.Application.Current.Shutdown();
        Environment.Exit(0);
        base.OnClosed(e);
    }
}
