// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System;
using System.Windows;
using Bindables.Wpf;

namespace GroomWise.Views.Templates;

public partial class NavItemTemplate
{
    public NavItemTemplate()
    {
        InitializeComponent();
    }

    [DependencyProperty(typeof(object))]
    public static readonly DependencyProperty IconProperty;

    [DependencyProperty(typeof(string))]
    public static readonly DependencyProperty TooltipProperty;

    [DependencyProperty(typeof(bool))]
    public static readonly DependencyProperty SelectedProperty;

    [DependencyProperty(typeof(Type))]
    public static readonly DependencyProperty PageContextProperty;
}
