// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Views.Dialogs;

public partial class LoginView : ILoginView
{
    private readonly ILogger _logger;

    public LoginView(ILoginViewModel viewModel, ILogger logger)
    {
        InitializeComponent();
        _logger = logger;
        DataContext = viewModel;
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
        _logger.Log(this, "Exiting application environment");
        Application.Current.Shutdown();
        Environment.Exit(0);
        base.OnClosed(e);
    }

    private void UsernameBox_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
            PasswordBox.Focus();
    }
}
