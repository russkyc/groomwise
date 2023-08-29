// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Application.Observables;
using GroomWise.Domain.Entities;
using Mapster;
using Swordfish.NET.Collections;

namespace GroomWise.Application.Mappers;

public static class CustomerMapper
{
    public static ObservableCustomer ToObservable(this Customer customer)
    {
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
