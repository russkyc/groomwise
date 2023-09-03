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

using GroomWise.Infrastructure.Logging.Interfaces;
using Injectio.Attributes;

namespace GroomWise.Infrastructure.Logging;

[RegisterSingleton<ILogger, ConsoleLogger>]
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
