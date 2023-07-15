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
        // Arrange
        _customerController.Add(firstName: "fn", middleName: "mn", lastName: "ln", suffix: "jr");

        // Act
        var savedCustomer = _customerController.FindByName(firstName: "fn");

        // Assert
        Assert.NotNull(savedCustomer);
    }

    [Fact]
    private void Update_Customer_Entity()
    {
        // Arrange
        _customerController.Add(firstName: "fn", middleName: "mn", lastName: "ln", suffix: "jr");

        // Act
        _customerController.Update(
            customer: _customerController.FindByName("fn"),
            set: customer => customer.FirstName = "nf"
        );

        var savedCustomer = _customerController.FindByName(firstName: "nf");

        // Assert
        Assert.NotNull(savedCustomer);
    }

    [Fact]
    private void Delete_Customer_Entity()
    {
        // Arrange
        _customerController.Add(firstName: "fn", middleName: "mn", lastName: "ln", suffix: "jr");

        // Act
        _customerController.Delete(customer: _customerController.FindByName("fn"));

        var savedCustomer = _customerController.FindByName(firstName: "nf");

        // Assert
        Assert.Null(savedCustomer);
    }
}
