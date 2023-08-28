// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Infrastructure.Navigation.Interfaces;

namespace GroomWise.Infrastructure.Navigation;

public class NavigationService : INavigationService
{
    private readonly SynchronizationContext? _synchronizationContext;
    private readonly Dictionary<Enum, object?>? _views;
    private static INavigationService? _instance;
    private static IWindow? _currentWindow;
    private static readonly object Lock = new();

    public static INavigationService? Instance
    {
        get
        {
            lock (Lock)
            {
                return _instance;
            }
        }
    }

    public IWindow CurrentWindow
    {
        get
        {
            lock (Lock)
            {
                return _currentWindow!;
            }
        }
    }

    public NavigationService(SynchronizationContext? context, IWindow? mainWindow)
    {
        if (context is not null)
        {
            _synchronizationContext = context;
            _views = new();
            _currentWindow = mainWindow;
        }
    }

    public void Add(Enum key, IWindow instance)
    {
        _views?.Add(key, instance);
    }

    public void Add(Enum key, IPage instance)
    {
        _views?.Add(key, instance);
    }

    public void Navigate(Enum key)
    {
        _synchronizationContext?.Send(
            _ =>
            {
                if (_views?[key] is IWindow window)
                {
                    window.Show();
                    _currentWindow?.Hide();
                    _currentWindow = window;
                }
            },
            null
        );
    }

    public static void Initialize(SynchronizationContext context, IWindow currentWindow)
    {
        lock (Lock)
        {
            _instance = new NavigationService(context, currentWindow);
        }
    }
}
