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
using System.Windows;
using Bindables.Wpf;
using GroomWise.Domain.Enums;

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

    [DependencyProperty(typeof(Role))]
    public static readonly DependencyProperty RoleProperty;
}
