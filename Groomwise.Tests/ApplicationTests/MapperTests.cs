// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Application.Mappers;
using GroomWise.Domain.Entities;

namespace Groomwise.Tests.ApplicationTests;

public class MapperTests
{
    [Fact]
    private void TestMap()
    {
        // Arrange
        var customer = new Customer { FirstName = "Customer" };

        // Act
        var observable = CustomerMapper.ToObservable(customer);

        // Assert
        Assert.Equal(customer.FirstName, observable.FirstName);
    }
}
