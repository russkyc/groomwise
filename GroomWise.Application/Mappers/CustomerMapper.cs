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

public static class CustomerMapper
{
    public static ObservableCustomer ToObservable(this Customer c)
    {
        return new ObservableCustomer
        {
            Id = c.Id,
            FullName = c.FullName,
            Address = c.Address,
            ContactNumber = c.ContactNumber,
            Email = c.Email,
            Pets = c.Pets?.ConvertAll(pet => pet.ToObservable()).AsObservableCollection(),
            Appointments = c.Appointments?
                .ConvertAll(appointment => appointment.ToObservable())
                .AsObservableCollection()
        };
    }

    public static Customer ToEntity(this ObservableCustomer o)
    {
        return new Customer
        {
            Id = o.Id,
            FullName = o.FullName,
            Address = o.Address,
            ContactNumber = o.ContactNumber,
            Email = o.Email,
            Pets = o.Pets.ToList().ConvertAll(pet => pet.ToEntity()),
            Appointments = o.Appointments
                .ToList()
                .ConvertAll(appointment => appointment.ToEntity())
        };
    }
}
