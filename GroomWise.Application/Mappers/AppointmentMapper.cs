// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Application.Observables;
using GroomWise.Domain.Entities;
using Mapster;

namespace GroomWise.Application.Mappers;

public static class AppointmentMapper
{
    public static ObservableAppointment ToObservable(this Appointment appointment)
    {
        return appointment.Adapt<ObservableAppointment>();
    }
}
