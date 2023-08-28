// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Infrastructure.Configuration.Interfaces;
using GroomWise.Infrastructure.Theming.Interfaces;
using Injectio.Attributes;
using MvvmGen;

namespace GroomWise.Application.ViewModels;

[ViewModel]
[Inject(typeof(IThemeManagerService))]
[Inject(typeof(IConfigurationService))]
[RegisterSingleton]
public partial class SettingsViewModel
{
    [Property]
    private IConfigurationService _configuration;

    partial void OnInitialize()
    {
        Configuration = ConfigurationService;
    }

}
