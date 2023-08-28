// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

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
