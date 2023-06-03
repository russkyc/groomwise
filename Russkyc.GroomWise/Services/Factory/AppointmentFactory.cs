// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Factory;

public class AppointmentFactory : IAppointmentFactory
{
    public Appointment Create(Action<Appointment>? builder = null)
    {
        var appointment = new Appointment();
        builder!(appointment);
        return appointment;
    }
}
