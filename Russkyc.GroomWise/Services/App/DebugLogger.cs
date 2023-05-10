// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.App;

public class DebugLogger : ILogger
{
    private readonly bool _isLoggingEnabled;

    public DebugLogger(IConfigurationService configurationService)
    {
        _isLoggingEnabled = configurationService.Config.ReadBoolean("Debugging", "Logging");
    }
    public void Log(string message)
    {
        if(_isLoggingEnabled) Debug.WriteLine(message);
    }

    public void Log(object sender, string message)
    {
        if (_isLoggingEnabled) Debug.WriteLine($"{sender.GetType()}: {message}");
    }
}