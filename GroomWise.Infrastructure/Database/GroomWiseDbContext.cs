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

using GroomWise.Domain.Entities;
using GroomWise.Infrastructure.Database.Interfaces;
using Injectio.Attributes;

namespace GroomWise.Infrastructure.Database;

[RegisterSingleton]
public class GroomWiseDbContext : DbContext
{
    public GroomWiseDbContext(IDbStore dbStore)
        : base(dbStore)
    {
        Pets = new(DataStore);
        Products = new(DataStore);
        Accounts = new(DataStore);
        Employees = new(DataStore);
        Customers = new(DataStore);
        Appointments = new(DataStore);
        GroomingServices = new(DataStore);
    }

    public readonly DbSet<Pet> Pets;
    public readonly DbSet<Account> Accounts;
    public readonly DbSet<Product> Products;
    public readonly DbSet<Customer> Customers;
    public readonly DbSet<Employee> Employees;
    public readonly DbSet<Appointment> Appointments;
    public readonly DbSet<GroomingService> GroomingServices;
}
