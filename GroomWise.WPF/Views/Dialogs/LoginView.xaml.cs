// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System;
using System.Linq;
using System.Windows.Input;
using GroomWise.Application.ViewModels;
using Russkyc.DependencyInjection.Attributes;
using Russkyc.DependencyInjection.Enums;

namespace GroomWise.Views.Dialogs;

[Service(Scope.Singleton)]
public partial class LoginView
{
    public LoginView(ILoginViewModel vm)
    {
        DataContext = vm;
        InitializeComponent();
        ClearFields();
    }

    public void ClearFields()
    {
        UsernameBox.Clear();
        PasswordBox.Clear();
        UsernameBox.Focus();
    }

    public void ClearFields(params string[] fields)
    {
        if (fields.Any(field => UsernameBox.Name.Contains(field)))
            UsernameBox.Clear();
        if (fields.Any(field => PasswordBox.Name.Contains(field)))
            PasswordBox.Clear();
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
}
