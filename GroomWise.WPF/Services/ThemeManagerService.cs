// GroomWise
// Copyright (C) 2023  John Russell C. Camo (@russkyc)
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY

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
