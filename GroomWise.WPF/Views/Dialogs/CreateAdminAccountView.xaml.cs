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
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GroomWise.Application.ViewModels;
using GroomWise.Infrastructure.Navigation.Interfaces;
using Injectio.Attributes;

namespace GroomWise.Views.Dialogs;

[RegisterTransient]
public partial class CreateAdminAccountView : IWindow
{
    public CreateAdminAccountView(LoginViewModel vm)
    {
        DataContext = vm;
        InitializeComponent();
        UsernameBox.Focus();
    }

    protected override void OnClosed(EventArgs e)
    {
        System.Windows.Application.Current.Shutdown();
        Environment.Exit(0);
        base.OnClosed(e);
    }

    private void UsernameBox_OnKeyDown(object sender, KeyEventArgs keyEventArgs)
    {
        if (keyEventArgs.Key == Key.Enter)
            PasswordBox.Focus();
    }

    private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext != null)
        {
            ((dynamic)DataContext).Password = ((PasswordBox)sender).Password;
        }
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        PasswordBox.Clear();
        base.OnClosing(e);
    }
}
