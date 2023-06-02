﻿// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace Russkyc.GroomWise.Tests.Factories;

public class AccountFactoryTests
{
    [Theory]
    [InlineData(0, "email@email.com", "russkyc", "password")]
    [InlineData(0, "manager@groomwise.com", "manager", "manager")]
    void Create_Returns_New_Account_WithModifications(
        int id,
        string email,
        string username,
        string password
    )
    {
        // Setup
        var accountFactory = new AccountFactory();

        // Execute
        var result = accountFactory.Create(account =>
        {
            account.Id = id;
            account.Email = email;
            account.Username = username;
            account.Password = password;
        });

        // Assert
        Assert.Equal(id, result.Id);
        Assert.Equal(email, result.Email);
        Assert.Equal(username, result.Username);
        Assert.Equal(password, result.Password);
    }
}
