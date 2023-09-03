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

public static class CustomerMapper
{
    public static ObservableCustomer ToObservable(this Customer customer)
    {
        var mapper = TypeAdapterConfig<Customer, ObservableCustomer>.NewConfig();

        mapper.Map(dest => dest.Pets, src => src.Pets!.Select(pet => pet.ToObservable()).ToList());

        if (customer.Appointments is not null)
        {
            mapper.Map(
                dest => dest.Appointments,
                src => src.Appointments!.Select(appointment => appointment.ToObservable()).ToList()
            );
        }
        return customer.Adapt<ObservableCustomer>();
    }

    public static Customer ToEntity(this ObservableCustomer observableCustomer)
    {
        TypeAdapterConfig<ObservableCustomer, Customer>
            .NewConfig()
            .Map(dest => dest.Pets, src => src.Pets.Select(pet => pet.ToEntity()).ToList())
            .Map(
                dest => dest.Appointments,
                src => src.Appointments.Select(appointment => appointment.ToEntity()).ToList()
            );
        return observableCustomer.Adapt<Customer>();
    }
}
