// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.App;

public class ThemeManagerService : IThemeManagerService
{
    public void UseNightBaseTheme(bool night)
    {
        ThemeManager.Instance.SetBaseTheme(night ? "Dark" : "Light");
    }

    public void UseColorTheme(string name)
    {
        ThemeManager.Instance.SetColorTheme(name);
    }
}