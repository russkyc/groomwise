﻿// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Views.Dialogs;

public partial class EditCustomersView
{
    public EditCustomersView(object vm)
    {
        DataContext = vm;
        InitializeComponent();
    }
}
