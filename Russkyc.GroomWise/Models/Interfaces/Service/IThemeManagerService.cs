// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Interfaces.Service;

public interface IThemeManagerService
{
    bool DarkMode { get; set; }
    void UseDarkTheme(bool night);
    void UseColorTheme(string color);
    IList<string> GetColorThemes();
    IList<string> GetBaseThemes();
    void Reset();
}
