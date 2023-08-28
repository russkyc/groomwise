// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Infrastructure.Navigation.Interfaces;
using Injectio.Attributes;

namespace GroomWise.Infrastructure.Navigation;

[RegisterSingleton<INavigationService, NavigationService>]
public class NavigationService : INavigationService
{
    private SynchronizationContext? _synchronizationContext;
    private Dictionary<Enum, object?>? _views;
    private static IWindow? _currentWindow;
    private static IPage _currentPage;
    private static readonly object Lock = new();

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

    public IPage CurrentPage
    {
        get
        {
            lock (Lock)
            {
                return _currentPage;
            }
        }
        private set
        {
            lock (Lock)
            {
                _currentPage = value;
            }
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
                var view = _views?[key];
                if (view is IWindow window)
                {
                    window.Show();
                    _currentWindow?.Hide();
                    _currentWindow = window;
                    return;
                }

                if (view is IPage page)
                {
                    CurrentPage = page;
                }
            },
            null
        );
    }

    public void Initialize(SynchronizationContext? context, IWindow? mainWindow)
    {
        if (context is not null)
        {
            _synchronizationContext = context;
            _views = new();
            _currentWindow = mainWindow;
        }
    }
}
