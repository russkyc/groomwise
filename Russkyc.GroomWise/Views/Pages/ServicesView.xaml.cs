﻿// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace Russkyc.GroomWise.Views.Pages;

public partial class ServicesView : IView
{
    public ServicesView(IServicesViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}