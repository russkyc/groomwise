// GroomWise
// Copyright (C) 2023  John Russell C. Camo (@russkyc)
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY

using GroomWise.Application.Observables;
using GroomWise.Domain.Entities;
using Mapster;

namespace GroomWise.Application.Mappers;

public static class AppointmentMapper
{
    public static ObservableAppointment ToObservable(this Appointment appointment)
    {
        var config = TypeAdapterConfig<Appointment, ObservableAppointment>.NewConfig();
        if (appointment.Services is not null)
        {
            config.Map(
                dest => dest.Services,
                src =>
                    src.Services
                        .Select(
                            groomingService =>
                                new ObservableAppointmentService
                                {
                                    GroomingService = groomingService.ToObservable()
                                }
                        )
                        .ToList()
            );
        }

        if (appointment.Employees is not null)
        {
            config.Map(
                dest => dest.Employees,
                src => src.Employees.Select(employee => employee.ToObservable()).ToList()
            );
        }

        if (appointment.Customer is not null)
        {
            config.Map(dest => dest.Customer, src => src.Customer.ToObservable());
        }

        if (appointment.Pet is not null)
        {
            config.Map(dest => dest.Pet, src => src.Pet.ToObservable());
        }
        return appointment.Adapt<ObservableAppointment>();
    }

    public static Appointment ToEntity(this ObservableAppointment observableAppointment)
    {
        TypeAdapterConfig<ObservableAppointment, Appointment>
            .NewConfig()
            .Map(
                dest => dest.Services,
                src =>
                    src.Services
                        .Select(appointmentService => appointmentService.GroomingService)
                        .ToList()
            )
            .Map(
                dest => dest.Employees,
                src =>
                    src.Employees
                        .Select(observableEmployee => observableEmployee.ToEntity())
                        .ToList()
            )
            .Map(dest => dest.Customer, src => src.Customer.ToEntity())
            .Map(dest => dest.Pet, src => src.Pet.ToEntity());
        return observableAppointment.Adapt<Appointment>();
    }
}
