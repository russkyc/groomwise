// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Windows.Controls;
using GroomWise.Models.Enums;

namespace Russkyc.GroomWise.Tests.Factories;

public class NavItemFactoryTests
{
    [Theory]
    [InlineData(
        0,
        "dashboard",
        typeof(UserControl),
        true,
        "dashboard",
        new[] { EmployeeType.Admin, EmployeeType.Manager }
    )]
    [InlineData(
        1,
        "employees",
        typeof(UserControl),
        true,
        "employees",
        new[] { EmployeeType.Manager }
    )]
    void Create_Returns_New_NavItem_WithModifications(
        int id,
        string name,
        Type page,
        bool selected,
        string shortName,
        EmployeeType[] accountTypes
    )
    {
        // Setup
        var accountFactory = new NavItemFactory();

        // Execute
        var result = accountFactory.Create(navItem =>
        {
            navItem.Id = id;
            navItem.Name = name;
            navItem.Page = page;
            navItem.Selected = selected;
            navItem.ShortName = shortName;
            navItem.AccountTypes = accountTypes;
        });

        // Assert
        Assert.Equal(id, result.Id);
        Assert.Equal(name, result.Name);
        Assert.Equal(page, result.Page);
        Assert.Equal(selected, result.Selected);
        Assert.Equal(shortName, result.ShortName);
        Assert.Equal(accountTypes, result.AccountTypes);
    }
}
