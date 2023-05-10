// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.App;

public class DebugAndFileLogger : ILogger
{
    private readonly string _path;
    private readonly bool _isLoggingEnabled;

    public DebugAndFileLogger(IConfigurationService configurationService)
    {
        _path = configurationService.Config.ReadString("Debugging", "LogFile");
        _isLoggingEnabled = configurationService.Config.ReadBoolean("Debugging", "Logging");

        if (_isLoggingEnabled)
            Log(this, "Starting application environment");
    }

    public void Log(string message)
    {
        if (_isLoggingEnabled)
        {
            Debug.WriteLine(message);
            _path.StreamAppend($"{Environment.NewLine}[{DateTime.Now:yy-MM-dd HH:mm:ss}]{message}");
        }
    }

    public void Log(object sender, string message)
    {
        if (_isLoggingEnabled)
        {
            Debug.WriteLine($"{sender.GetType()}: {message}");
            _path.StreamAppend(
                $"{Environment.NewLine}[{DateTime.Now:yy-MM-dd HH:mm:ss}]{sender.GetType()}: {message}"
            );
        }
    }
}
