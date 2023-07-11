// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Infrastructure.Data;

namespace GroomWise.Infrastructure.Tests;

public class DataAccessProviderTests
{
    [Fact]
    private void Create_And_Read_Entity()
    {
        // Arrange
        var dataAccessProvider = new DataAccessProvider(new MemoryStream());
        var entity = new TestEntity { StringProperty = "New Entity" };

        // Act
        var id = dataAccessProvider.Add(entity);
        var saved = dataAccessProvider.Get<TestEntity>(id);

        // Assert
        Assert.NotNull(saved);
    }

    [Fact]
    private void Create_And_Read_Multiple()
    {
        // Arrange
        var dataAccessProvider = new DataAccessProvider(new MemoryStream());
        var collection = new[]
        {
            new TestEntity { StringProperty = "Entity 1" },
            new TestEntity { StringProperty = "Entity 2" },
            new TestEntity { StringProperty = "Entity 3" }
        };

        // Act
        var insertedRows = dataAccessProvider.AddMany(collection);

        // Assert
        Assert.Equal(collection.Length, insertedRows);
    }

    [Fact]
    private void Get_All()
    {
        // Arrange
        var dataAccessProvider = new DataAccessProvider(new MemoryStream());
        var collection = new[]
        {
            new TestEntity { StringProperty = "Entity 1" },
            new TestEntity { StringProperty = "Entity 2" },
            new TestEntity { StringProperty = "Entity 3" }
        };
        dataAccessProvider.AddMany(collection);

        // Act
        var saved = dataAccessProvider.GetAll<TestEntity>();

        // Assert
        Assert.Equivalent(collection, saved);
    }

    [Fact]
    private void Find_Single()
    {
        // Arrange
        var dataAccessProvider = new DataAccessProvider(new MemoryStream());
        var entity = new TestEntity { StringProperty = "New Entity" };

        dataAccessProvider.Add(entity);

        // Act
        var saved = dataAccessProvider.Find<TestEntity>(
            t => t.StringProperty!.Equals("New Entity")
        );

        // Assert
        Assert.NotNull(saved);
        Assert.Equivalent(entity, saved);
    }

    [Fact]
    private void Find_Multiple()
    {
        // Arrange
        var dataAccessProvider = new DataAccessProvider(new MemoryStream());
        var collection = new[]
        {
            new TestEntity { StringProperty = "Entity 1" },
            new TestEntity { StringProperty = "Entity 2" },
            new TestEntity { StringProperty = "Entity 3" }
        };
        dataAccessProvider.AddMany(collection);

        // Act
        var saved = dataAccessProvider.FindMany<TestEntity>(
            t => t.StringProperty!.Contains("Entity")
        );

        // Assert
        Assert.NotNull(saved);
        Assert.Equal(collection.Length, saved.Count());
    }

    [Fact]
    private void Update_Single()
    {
        // Arrange
        var dataAccessProvider = new DataAccessProvider(new MemoryStream());
        var entity = new TestEntity { StringProperty = "Entity" };

        // Act
        var id = dataAccessProvider.Add(entity);
        var saved = dataAccessProvider.Get<TestEntity>(id);

        saved.StringProperty = "Entity Updated";
        var updated = dataAccessProvider.Update(saved);

        // Assert
        Assert.True(updated);
    }

    [Fact]
    private void Update_Multiple()
    {
        // Arrange
        var dataAccessProvider = new DataAccessProvider(new MemoryStream());
        var collection = new[]
        {
            new TestEntity { StringProperty = "Entity 1" },
            new TestEntity { StringProperty = "Entity 2" },
            new TestEntity { StringProperty = "Entity 3" }
        };

        dataAccessProvider.AddMany(collection);

        // Act
        var affectedRows = dataAccessProvider.UpdateMany<TestEntity>(
            t => new TestEntity { StringProperty = "New Entity" },
            t => t.StringProperty!.Contains("Entity")
        );

        // Assert
        Assert.Equal(collection.Length, affectedRows);
    }

    [Fact]
    private void Delete_Single()
    {
        // Arrange
        var dataAccessProvider = new DataAccessProvider(new MemoryStream());
        var entity = new TestEntity { StringProperty = "Entity" };

        var id = dataAccessProvider.Add(entity);

        // Act
        var removed = dataAccessProvider.Delete<TestEntity>(id);

        // Assert
        Assert.True(removed);
    }

    [Fact]
    private void Delete_Multiple()
    {
        // Arrange
        var dataAccessProvider = new DataAccessProvider(new MemoryStream());
        var collection = new[]
        {
            new TestEntity { StringProperty = "Entity 1" },
            new TestEntity { StringProperty = "Entity 2" },
            new TestEntity { StringProperty = "Entity 3" }
        };

        dataAccessProvider.AddMany(collection);

        // Act
        var affectedRows = dataAccessProvider.DeleteMany<TestEntity>(
            t => t.StringProperty!.Contains("Entity")
        );

        // Assert
        Assert.Equal(collection.Length, affectedRows);
    }
}

class TestEntity
{
    public Guid Id { get; set; }
    public string? StringProperty { get; set; }
}
