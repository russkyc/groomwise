// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Factory;

public class AppointmentFactory : IAppointmentFactory
{
    public Appointment Create()
    {
        return new Appointment();
    }

    public Appointment Create(params object[] values)
    {
        return new Appointment
        {
            Title = (string)values[0],
            Description = (string)values[1],
            Date = (DateTime)values[2],
            PetId = (int)values[3],
            CustomerId = (int)values[4],
            EmployeeId = (int)values[5],
            GroomingServiceId = (int)values[6],
            AppointmentStatus = (int)values[7]
        };
    }
}
