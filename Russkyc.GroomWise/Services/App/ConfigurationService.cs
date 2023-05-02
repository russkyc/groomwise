// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using Ini.Net;

namespace GroomWise.Services.App;

public class ConfigurationService : IConfigurationService
{
    public ConfigurationService()
    {
        Config = new IniFile("GroomWise.ini");
    }

    public IniFile Config { get; set; }
}