// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Entities;
using GroomWise.Service.Database;

namespace Groomwise.Tests.ServiceTests;

public class DatabaseTests
{
    [Fact]
    private void Add_And_Get_Entity()
    {
        // Arrange
        var db = new GroomWiseDbContext(new DbStore(":memory:"));
        var entity = new Account { Username = "TestUser", Password = "TasPass" };

        // Act
        var result = db.Accounts.Insert(entity);
        var inserted = db.Accounts.Get(account => account.Id.Equals(result));

        // Assert
        Assert.NotNull(inserted);
    }
}
