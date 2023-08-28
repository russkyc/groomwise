// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Infrastructure.Navigation.Interfaces;

public interface INavigationService
{
    IWindow CurrentWindow { get; }
    IPage CurrentPage { get; }
    void Add(Enum key, IWindow instance);
    void Add(Enum key, IPage instance);
    void Navigate(Enum key);
    void Initialize(SynchronizationContext? context, IWindow? mainWindow);
}
