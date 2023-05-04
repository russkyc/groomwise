// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

#region

using Ini.Net;

#endregion

namespace GroomWise.Services.App;

public class ConfigurationService : IConfigurationService
{
    public string Key { get; set; }
    public IniFile Config { get; set; }

    public ConfigurationService()
    {
        Key = "@Tcu700600";
        Config = new IniFile("GroomWise.ini");
    }
}