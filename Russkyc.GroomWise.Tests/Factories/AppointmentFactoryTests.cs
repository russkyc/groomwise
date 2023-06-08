// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace Russkyc.GroomWise.Tests.Factories;

public class AppointmentFactoryTests
{
    [Theory]
    [InlineData(0, "Grooming Service", "Cut and Trim")]
    [InlineData(1, "Operation", "Infection treatment")]
    void Create_Returns_New_Employee_WithModifications(int id, string title, string description)
    {
        // Setup
        var appointmentFactory = new AppointmentFactory();

        // Execute
        var result = appointmentFactory.Create(appointment =>
        {
            appointment.Id = id;
            appointment.Title = title;
            appointment.Description = description;
        });

        // Assert
        Assert.Equal(id, result.Id);
        Assert.Equal(title, result.Title);
        Assert.Equal(description, result.Description);
    }
}
