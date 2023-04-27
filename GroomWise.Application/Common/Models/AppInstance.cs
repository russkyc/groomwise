// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using GroomWise.Core.Interfaces;
using GroomWise.Core.Models;

namespace GroomWise.Application.Common.Models
{
    public class AppInstance : ObservableObject, IAppInstance
    {
        public List<NavItem> NavItems { get; set; }

        public AppInstance()
        {
            NavItems = new List<NavItem>();
        }
    }
}