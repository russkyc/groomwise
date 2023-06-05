// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace Russkyc.GroomWise.Tests.Factories;

public class AppointmentFactoryTests
{
    [Theory]
    [InlineData(0, "Grooming Service", "Cut and Trim", 0, 0, 0, 0, 0)]
    [InlineData(1, "Operation", "Infection treatment", 0, 0, 0, 0, 0)]
    void Create_Returns_New_Employee_WithModifications(
        int id,
        string title,
        string description,
        int appointmentStatus,
        int employeeId,
        int customerId,
        int groomingServiceId,
        int petId
    )
    {
        // Setup
        var accountFactory = new AppointmentFactory();

        // Execute
        var result = accountFactory.Create(appointment =>
        {
            appointment.Id = id;
            appointment.Title = title;
            appointment.Description = description;
            appointment.AppointmentStatus = appointmentStatus;
            appointment.EmployeeId = employeeId;
            appointment.CustomerId = customerId;
            appointment.GroomingServiceId = groomingServiceId;
            appointment.PetId = petId;
        });

        // Assert
        Assert.Equal(id, result.Id);
        Assert.Equal(title, result.Title);
        Assert.Equal(description, result.Description);
        Assert.Equal(appointmentStatus, result.AppointmentStatus);
        Assert.Equal(employeeId, result.EmployeeId);
        Assert.Equal(customerId, result.CustomerId);
        Assert.Equal(groomingServiceId, result.GroomingServiceId);
        Assert.Equal(petId, result.PetId);
    }
}
