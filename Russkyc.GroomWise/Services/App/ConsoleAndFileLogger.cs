// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.App;

public class ConsoleAndFileLogger : ILogger
{
    private string _path;
    
    public ConsoleAndFileLogger(IConfigurationService configurationService)
    {
        _path = configurationService.Config.ReadString("AppSettings", "LogFile");
        Log(this,"Starting application environment");

    }
    public void Log(string message)
    {
        Console.WriteLine(message);
        _path.StreamAppend($"{Environment.NewLine}[{DateTime.Now:yy-MM-dd HH:mm:ss}] {message}");
    }

    public void Log(object sender, string message)
    {
        Console.WriteLine($"{sender.GetType()}: {message}");
        _path.StreamAppend($"{Environment.NewLine}[{DateTime.Now:yy-MM-dd HH:mm:ss}]{sender.GetType()}: {message}");
    }
}