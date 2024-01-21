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
    public readonly DbSet<Account> Accounts;
    public readonly DbSet<Appointment> Appointments;
    public readonly DbSet<Customer> Customers;
    public readonly DbSet<Employee> Employees;
    public readonly DbSet<GroomingService> GroomingServices;

    public readonly DbSet<Pet> Pets;
    public readonly DbSet<Product> Products;

    public GroomWiseDbContext(IDbStore dbStore)
        : base(dbStore)
    {
        Pets = new DbSet<Pet>(DataStore);
        Products = new DbSet<Product>(DataStore);
        Accounts = new DbSet<Account>(DataStore);
        Employees = new DbSet<Employee>(DataStore);
        Customers = new DbSet<Customer>(DataStore);
        Appointments = new DbSet<Appointment>(DataStore);
        GroomingServices = new DbSet<GroomingService>(DataStore);
    }
}