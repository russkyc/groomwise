// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Views;

public partial class MainView : IMainView
{
    public MainView(IMainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    public void ClearFields()
    {
        throw new NotImplementedException();
    }

    public void ClearFields(params string[] fields)
    {
        throw new NotImplementedException();
    }

    protected override void OnClosed(EventArgs e)
    {
        BuilderServices.Resolve<ILogger>().Log(this, "Exiting application environment");
        base.OnClosed(e);
        Application.Current.Shutdown();
        Environment.Exit(0);
    }
}
