// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GroomWise.Application.ViewModels;
using GroomWise.Infrastructure.Navigation.Interfaces;
using Injectio.Attributes;

namespace GroomWise.Views.Dialogs;

[RegisterSingleton]
public partial class LoginView : IWindow
{
    public LoginView(LoginViewModel vm)
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
}
