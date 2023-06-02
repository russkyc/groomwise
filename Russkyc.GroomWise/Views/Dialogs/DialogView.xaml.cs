// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

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
