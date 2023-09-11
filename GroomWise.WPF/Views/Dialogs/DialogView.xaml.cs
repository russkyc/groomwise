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

namespace GroomWise.Views.Dialogs;

public partial class DialogView
{
    public MessageBoxButton Buttons { get; set; }
    public string? MessageBoxText { get; set; }
    public string? Caption { get; set; }

    public DialogView()
    {
        Buttons = MessageBoxButton.YesNo;
        InitializeComponent();
        BuildButtons();
    }

    public DialogView(string messageBoxText, string caption)
    {
        Buttons = MessageBoxButton.YesNo;
        MessageBoxText = messageBoxText;
        Caption = caption;
        InitializeComponent();
        BuildButtons();
    }

    public DialogView(string messageBoxText, string caption, MessageBoxButton button)
    {
        MessageBoxText = messageBoxText;
        Caption = caption;
        Buttons = button;
        InitializeComponent();
        BuildButtons();
    }

    private void ButtonYes_OnClick(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }

    private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }

    void BuildButtons()
    {
        ButtonYes.Visibility =
            Buttons == MessageBoxButton.YesNo || Buttons == MessageBoxButton.YesNoCancel
                ? Visibility.Visible
                : Visibility.Collapsed;

        ButtonNo.Visibility =
            Buttons == MessageBoxButton.YesNo || Buttons == MessageBoxButton.YesNoCancel
                ? Visibility.Visible
                : Visibility.Collapsed;
        ButtonOk.Visibility =
            Buttons == MessageBoxButton.OK || Buttons == MessageBoxButton.OKCancel
                ? Visibility.Visible
                : Visibility.Collapsed;
        ButtonCancel.Visibility =
            Buttons == MessageBoxButton.YesNoCancel || Buttons == MessageBoxButton.OKCancel
                ? Visibility.Visible
                : Visibility.Collapsed;

        if (Buttons == MessageBoxButton.YesNo || Buttons == MessageBoxButton.YesNoCancel)
            ButtonYes.Focus();
        if (Buttons == MessageBoxButton.OK || Buttons == MessageBoxButton.OKCancel)
            ButtonOk.Focus();
    }
}
