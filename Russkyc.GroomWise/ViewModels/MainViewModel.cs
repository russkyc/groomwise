// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Linq;

namespace Russkyc.GroomWise.ViewModels;

public partial class MainViewModel : ViewModelBase, IMainViewModel
{
    [ObservableProperty]
    private IAppService _appService;

    [ObservableProperty]
    private IView? _view;
    
    public MainViewModel(IAppService appService)
    {
        _appService = appService;
    }

    [RelayCommand]
    private void GetView(string pageName)
    {
        _appService!.NavItems.First(item => item.Page == pageName).Selected = true;
        View = BuilderServices.Resolve(pageName) as IView;
    }
}