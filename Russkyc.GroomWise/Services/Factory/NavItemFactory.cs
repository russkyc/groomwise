// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Factory;

public class NavItemFactory : INavItemFactory
{
    public NavItem Create()
    {
        return new NavItem();
    }

    public NavItem Create(params object[] values)
    {
        return new NavItem
        {
            Name = (string)values[0],
            Page = (Type)values[1],
            Icon = (object)values[2],
            Selected = (bool)values[3],
            AccountTypes = (EmployeeType[])values[4]
        };
    }
}