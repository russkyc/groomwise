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

using System;
using GroomWise.Application.ViewModels;
using GroomWise.Infrastructure.Navigation.Interfaces;
using Injectio.Attributes;
using org.russkyc.moderncontrols;

namespace GroomWise.Views;

[RegisterTransient]
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
