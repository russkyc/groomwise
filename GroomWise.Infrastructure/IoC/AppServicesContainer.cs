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
using Injectio.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace GroomWise.Infrastructure.IoC;

[RegisterSingleton<IAppServicesContainer, AppServicesContainer>]
public class AppServicesContainer : IAppServicesContainer
{
    private IServiceProvider? _serviceProvider;

    public void AddContainer(IServiceProvider container)
    {
        _serviceProvider = container;
    }

    public T? GetService<T>()
    {
        return _serviceProvider!.GetService<T>();
    }

    public object? GetService(Type type)
    {
        return _serviceProvider!.GetService(type);
    }
}
