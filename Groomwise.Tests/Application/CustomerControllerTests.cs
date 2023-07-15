// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Application.Customers;
using GroomWise.Application.Customers.Interfaces;
using GroomWise.Domain.Entities;
using GroomWise.Infrastructure.Data;

namespace Groomwise.Tests.Application;

public class CustomerControllerTests
{
    private readonly ICustomerController _customerController = new CustomerController(
        new RepositoryProvider<Customer>(new DataAccessProvider(new MemoryStream()))
    );

    [Fact]
    private void Add_And_Get_Customer_By_Name()
    {
        // Arrange and Act
        _customerController.Add(firstName: "fn", middleName: "mn", lastName: "ln", suffix: "jr");

        // Assert
        Assert.NotNull(_customerController.Find(customer => customer.FirstName!.Equals("fn")));
    }

    [Fact]
    private void Get_All_Customers()
    {
        // Arrange
        _customerController.Add(
            firstName: "fn1",
            middleName: "mn1",
            lastName: "ln1",
            suffix: "jr1"
        );
        _customerController.Add(
            firstName: "fn2",
            middleName: "mn2",
            lastName: "ln2",
            suffix: "jr2"
        );
        _customerController.Add(
            firstName: "bn3",
            middleName: "mn3",
            lastName: "ln3",
            suffix: "jr3"
        );

        // Act
        var entities = _customerController.GetAll();
        var entitiesFiltered = _customerController.GetAll(
            filter: customer => customer.FirstName!.Contains("fn")
        );

        // Assert
        Assert.NotNull(entities);
        Assert.NotNull(entitiesFiltered);
        Assert.Equal(3, entities.Count());
        Assert.Equal(2, entitiesFiltered.Count());
    }

    [Fact]
    private void Update_Customer_Entity()
    {
        // Arrange
        _customerController.Add(firstName: "fn", middleName: "mn", lastName: "ln", suffix: "jr");

        // Act
        _customerController.Update(
            customer: _customerController.Find(customer => customer.FirstName!.Equals("fn")),
            set: customer => customer.FirstName = "nf"
        );

        // Assert
        Assert.NotNull(_customerController.Find(customer => customer.FirstName!.Equals("nf")));
    }

    [Fact]
    private void Delete_Customer_Entity()
    {
        // Arrange
        _customerController.Add(firstName: "fn", middleName: "mn", lastName: "ln", suffix: "jr");

        // Act
        _customerController.Delete(
            customer: _customerController.Find(customer => customer.FirstName!.Equals("fn"))
        );

        // Assert
        Assert.Null(_customerController.Find(customer => customer.FirstName!.Equals("fn")));
    }
}
