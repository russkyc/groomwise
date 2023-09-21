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

using GroomWise.Application.Extensions;
using GroomWise.Application.Observables;
using GroomWise.Domain.Entities;

namespace GroomWise.Application.Mappers;

public static class AppointmentMapper
{
    public static Appointment ToEntity(this ObservableAppointment o)
    {
        return new Appointment
        {
            Date = o.Date,
            StartTime = o.StartTime,
            EndTime = o.EndTime,
            Status = o.Status,
            Customer = o.Customer.ToEntity(),
            Pet = o.Pet?.ToEntity(),
            Services = o.Services?.Select(service => service.GroomingService?.ToEntity()).ToList(),
            Employees = o.Employees
                ?.Select(employee => employee?.ToEntity())
                .AsObservableCollection()
        };
    }

    public static ObservableAppointment ToObservable(this Appointment a)
    {
        return new ObservableAppointment
        {
            Id = a.Id,
            Date = a.Date,
            StartTime = a.StartTime,
            EndTime = a.EndTime,
            Status = a.Status,
            Customer = a.Customer.ToObservable(),
            Pet = a.Pet?.ToObservable(),
            Services = a.Services
                ?.Select(
                    service =>
                        new ObservableAppointmentService
                        {
                            GroomingService = service?.ToObservable()
                        }
                )
                .AsObservableCollection(),
            Employees = a.Employees
                ?.Select(employee => employee?.ToObservable())
                .AsObservableCollection()
        };
    }
}
