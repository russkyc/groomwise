// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Views.Dialogs;

public partial class AddCustomersView : IAddCustomersView
{
    public AddCustomersView(IAddCustomersViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }

    public void ClearFields()
    {
        throw new NotImplementedException();
    }

    public void ClearFields(params string[] fields)
    {
        throw new NotImplementedException();
    }
}
