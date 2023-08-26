// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System;
using System.ComponentModel;

namespace GroomWise.Views;

public partial class MainView
{
    public MainView()
    {
        InitializeComponent();
    }

    protected override void OnClosed(EventArgs e)
    {
        System.Windows.Application.Current.Shutdown();
        Environment.Exit(0);
        base.OnClosed(e);
    }
}
