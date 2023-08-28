// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using Microsoft.Extensions.DependencyInjection;
using MvvmGen.Events;

namespace GroomWise.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventAggregator(this IServiceCollection collection)
    {
        collection.AddSingleton<IEventAggregator, EventAggregator>();
        return collection;
    }
}
