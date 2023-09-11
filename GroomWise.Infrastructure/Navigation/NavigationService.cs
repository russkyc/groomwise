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

using GroomWise.Infrastructure.IoC.Interfaces;
using GroomWise.Infrastructure.Navigation.Interfaces;
using Injectio.Attributes;

namespace GroomWise.Infrastructure.Navigation;

[RegisterSingleton<INavigationService, NavigationService>]
public class NavigationService : INavigationService
{
    private readonly Dictionary<Enum, Type> _views;
    private readonly object _lock;
    private readonly IAppServicesContainer _container;

    private SynchronizationContext? _synchronizationContext;
    private IWindow? _currentWindow;

    public NavigationService(IAppServicesContainer container)
    {
        _container = container;
        _lock = new();
        lock (_lock)
        {
            _views = new Dictionary<Enum, Type>();
        }
    }

    public IWindow CurrentWindow
    {
        get { return _currentWindow!; }
    }

    public void Add(Enum key, Type type)
    {
        lock (_lock)
        {
            _views.Add(key, type);
        }
    }

    public void Navigate(Enum key, bool hidePrevious = true)
    {
        lock (_lock)
        {
            var view = _container.GetService(_views[key]);
            if (view is IWindow window)
            {
                window.Show();
                _synchronizationContext?.Send(
                    _ =>
                    {
                        if (hidePrevious)
                        {
                            _currentWindow?.Hide();
                        }
                        _currentWindow = window;
                    },
                    null
                );
            }
        }
    }

    public void Initialize(SynchronizationContext? context, IWindow? mainWindow)
    {
        if (context is not null)
        {
            lock (_lock)
            {
                _synchronizationContext = context;
                _currentWindow = mainWindow;
            }
        }
    }
}
