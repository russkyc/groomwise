// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Diagnostics;

namespace GroomWise.Services.App;

public class DebugLogger : ILogger
{
    public void Log(string message)
    {
        Debug.WriteLine(message);
    }

    public void Log(object sender, string message)
    {
        Debug.WriteLine($"{sender.GetType()}: {message}");
    }
}