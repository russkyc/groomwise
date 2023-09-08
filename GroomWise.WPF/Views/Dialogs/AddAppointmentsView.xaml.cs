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

using System.ComponentModel;
using GroomWise.Extensions;
using GroomWise.Infrastructure.Navigation.Interfaces;

namespace GroomWise.Views.Dialogs;

public partial class AddAppointmentsView : IWindow
{
    public AddAppointmentsView(object viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        WindowExtensions.CloseAllAndFocusMainView();
        base.OnClosing(e);
    }
}
