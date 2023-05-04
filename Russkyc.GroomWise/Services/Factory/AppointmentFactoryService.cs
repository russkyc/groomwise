// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Factory;

public class AppointmentFactoryService : IAppointmentFactoryService
{
    public Appointment Create()
    {
        return new Appointment();
    }

    public Appointment Create(params object[] values)
    {
        throw new NotImplementedException();
    }
}