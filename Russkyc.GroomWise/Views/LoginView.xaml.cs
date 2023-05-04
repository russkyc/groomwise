// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Views;

public partial class LoginView
{
    public LoginView(ILoginViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
        ClearFields();
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        if (!BuilderServices.Resolve<MainView>().IsVisible)
        {
            Application.Current.Shutdown();
            Environment.Exit(0);
        }
    }

    public void ClearFields()
    {
        UsernameBox.Clear();
        PasswordBox.Clear();
        UsernameBox.Focus();
    }
    public void ClearPasswords()
    {
        PasswordBox.Clear();
        PasswordBox.Focus();
    }
}