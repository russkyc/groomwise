// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Infrastructure.Theming.Interfaces;
using Injectio.Attributes;
using org.russkyc.moderncontrols.Helpers;

namespace GroomWise.Services;

[RegisterSingleton]
public class ThemeManagerService : IThemeManagerService
{
    public void SetDarkTheme(bool useDarkTheme)
    {
        ThemeManager.Instance.SetBaseTheme(useDarkTheme ? "Dark" : "Light");
    }

    public void SetColorTheme(string themeId)
    {
        ThemeManager.Instance.SetColorTheme(themeId);
    }
}
