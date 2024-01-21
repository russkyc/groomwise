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

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using GroomWise.Domain.Enums;

namespace GroomWise.Views.Dialogs;

public partial class AddAccountView
{
    public AddAccountView(object vm)
    {
        DataContext = vm;
        InitializeComponent();
    }

    public IEnumerable<Role> Roles => new[] { Role.Admin, Role.Manager, Role.Groomer };

    private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext != null) ((dynamic)DataContext).Password = ((PasswordBox)sender).Password;
    }
}