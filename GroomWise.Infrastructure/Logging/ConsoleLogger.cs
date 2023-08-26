// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Infrastructure.Logging.Interfaces;
using Russkyc.DependencyInjection.Attributes;
using Russkyc.DependencyInjection.Enums;

namespace GroomWise.Infrastructure.Logging;

[Service(Scope.Singleton, Registration.AsInterfaces)]
public class ConsoleLogger : ILogger
{
    public void Log(string message, string? sender = null)
    {
        if (sender is null)
        {
            Console.WriteLine(message);
        }
        Console.WriteLine($"{sender}: {message}");
    }
}
