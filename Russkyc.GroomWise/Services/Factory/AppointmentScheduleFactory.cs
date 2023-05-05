// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Factory;

public class AppointmentScheduleFactory : IAppointmentScheduleFactory
{
    public AppointmentSchedule Create()
    {
        return new AppointmentSchedule();
    }

    public AppointmentSchedule Create(params object[] values)
    {
        return new AppointmentSchedule
        {
            Day = (string)values[0],
            Time = (string)values[1],
            Date = (string)values[2],
            Title = (string)values[3],
            Description = (string)values[4]
        };
    }
}