// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Application.Observables;
using GroomWise.Domain.Entities;
using Mapster;

namespace GroomWise.Application.Mappers;

public static class CustomerMapper
{
    public static ObservableCustomer ToObservable(this Customer customer)
    {
        return customer.Adapt<ObservableCustomer>();
    }

    public static Customer ToEntity(this ObservableCustomer observableCustomer)
    {
        return observableCustomer.Adapt<Customer>();
    }
}
