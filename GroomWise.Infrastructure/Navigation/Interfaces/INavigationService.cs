// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Infrastructure.Navigation.Interfaces;

public interface INavigationService
{
    IWindow CurrentWindow { get; }
    void Add(Enum key, IWindow instance);
    void Navigate(Enum key, bool hidePrevious = true);
    void Initialize(SynchronizationContext? context, IWindow? mainWindow);
}
