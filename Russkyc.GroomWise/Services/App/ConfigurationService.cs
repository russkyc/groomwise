// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.App;

public class ConfigurationService : IConfigurationService
{
# if (DEBUG)
    public IniFile Config => new IniFile("appconfig-indev.ini");
# else
    public IniFile Config => new IniFile("appconfig.ini");
#endif
}
