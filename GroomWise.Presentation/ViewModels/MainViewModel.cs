// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using CommunityToolkit.Mvvm.ComponentModel;

namespace Russkyc.GroomWise.Desktop.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private string? _welcomeMessage;

    public MainViewModel()
    {
        WelcomeMessage = "Welcome to your MVVM App!";
    }
}