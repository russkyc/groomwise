// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace Russkyc.GroomWise.Tests.Factories;

public class EmployeeFactoryTests
{
    [Theory]
    [InlineData(0, "email@email.com", "russkyc", "password", 0, 0)]
    [InlineData(0, "manager@groomwise.com", "manager", "manager", 0, 1)]
    void Create_Returns_New_Employee_WithModifications(
        int id,
        string firstName,
        string middleName,
        string lastName,
        int addressId,
        int employeeType
    )
    {
        // Setup
        var accountFactory = new EmployeeFactory();

        // Execute
        var result = accountFactory.Create(account =>
        {
            account.Id = id;
            account.FirstName = firstName;
            account.MiddleName = middleName;
            account.LastName = lastName;
            account.AddressId = addressId;
            account.EmployeeType = employeeType;
        });

        // Assert
        Assert.Equal(id, result.Id);
        Assert.Equal(firstName, result.FirstName);
        Assert.Equal(middleName, result.MiddleName);
        Assert.Equal(lastName, result.LastName);
        Assert.Equal(addressId, result.AddressId);
        Assert.Equal(employeeType, result.EmployeeType);
    }
}
