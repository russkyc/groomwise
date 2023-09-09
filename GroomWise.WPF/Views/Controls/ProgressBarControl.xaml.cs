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

using System.Windows;
using Bindables.Wpf;

namespace GroomWise.Views.Controls;

public partial class ProgressBarControl
{
    [DependencyProperty(typeof(double))]
    public static readonly DependencyProperty ProgressProperty;

    public ProgressBarControl()
    {
        InitializeComponent();
    }
}
