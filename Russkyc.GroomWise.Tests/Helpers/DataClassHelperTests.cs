// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Models.Entities;

namespace Russkyc.GroomWise.Tests.Helpers;

public class DataClassHelperTests
{
    [Fact]
    void HasSameValues_Returns_Equal()
    {
        // Setup
        var control = new Account
        {
            Username = "username",
            Password = "password",
            Email = "email",
            EmployeeId = 0
        };
        var subject = new Account
        {
            Username = "username",
            Password = "password",
            Email = "email",
            EmployeeId = 0
        };
        // Execute
        var result = subject.HasSameValues(control);
        // Assert
        Assert.True(result);
    }

    [Fact]
    void HasSameValues_Returns_NotEqual()
    {
        // Setup
        var control = new Account
        {
            Username = "username",
            Password = "password",
            Email = "email",
            EmployeeId = 0
        };
        var subject = new Account
        {
            Username = "username",
            Password = "password",
            Email = "email",
            EmployeeId = 2
        };
        // Execute
        var result = subject.HasSameValues(control);
        // Assert
        Assert.False(result);
    }
}
