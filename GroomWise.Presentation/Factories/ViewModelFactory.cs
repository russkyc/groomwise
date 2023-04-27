// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Core.Interfaces;
using Russkyc.GroomWise.Desktop.ViewModels;

namespace Russkyc.GroomWise.Desktop.Factories;

public static class ViewModelFactory
{
    public static MainViewModel CreateMainViewModel(IAppInstance app)
    {
        return new MainViewModel(app);
    }
}