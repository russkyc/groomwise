// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Views;

public partial class LoginView : ILoginView
{
    public LoginView(ILoginViewModel viewModel)
    {
        DataContext = viewModel;
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
        if (fields.Any(field => UsernameBox
                .Name
                .Contains(field)))
        {
            UsernameBox.Clear();
            UsernameBox.Focus();
        }

        if (fields.Any(field => PasswordBox
                .Name
                .Contains(field)))
        {
            PasswordBox.Clear();
            PasswordBox.Focus();
        }
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        BuilderServices.Resolve<ILogger>()
            .Log(this, "Exiting application environment");
        Application.Current
            .Shutdown();
        Environment.Exit(0);
    }
}