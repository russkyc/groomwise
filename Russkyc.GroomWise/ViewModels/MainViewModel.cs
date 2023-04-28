// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Linq;
using Russkyc.GroomWise.Models.Interfaces;

namespace Russkyc.GroomWise.ViewModels;

public partial class MainViewModel : ViewModelBase, IMainViewModel
{
    [ObservableProperty]
    private IAppInstance _appInstance;

    [ObservableProperty]
    private IView? _view;
    
    public MainViewModel(IAppInstance appInstance)
    {
        _appInstance = appInstance;
    }

    [RelayCommand]
    private void GetView(string pageName)
    {
        _appInstance!.NavItems.First(item => item.Page == pageName).Selected = true;
        View = BuilderServices.Resolve(pageName) as IView;
    }
}