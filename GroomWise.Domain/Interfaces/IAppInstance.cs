// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Collections.Generic;
using GroomWise.Core.Models;

namespace GroomWise.Core.Interfaces
{
    public interface IAppInstance
    {
        List<NavItem> NavItems { get; set; }
    }
}