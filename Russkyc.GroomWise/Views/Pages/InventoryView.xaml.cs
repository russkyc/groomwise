// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Windows.Controls;

namespace GroomWise.Views.Pages;

public partial class InventoryView : IInventoryView
{
    public InventoryView(IInventoryViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    public void Invalidate()
    {
        throw new NotImplementedException();
    }
}
