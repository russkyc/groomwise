// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Interfaces.Service;

public interface IApplicationService
{
    SynchronizedObservableCollection<NavItem>? NavItems { get; set; }


    /// <summary>
    /// Builds the nav items collection(App Navigation)
    /// </summary>
    void BuildNavItems();
}
