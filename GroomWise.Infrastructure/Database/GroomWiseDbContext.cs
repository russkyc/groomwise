// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Entities;
using Russkyc.DependencyInjection.Attributes;
using Russkyc.DependencyInjection.Enums;

namespace GroomWise.Infrastructure.Database;

[Service(Scope.Singleton)]
public class GroomWiseDbContext : DbContext
{
    public GroomWiseDbContext()
    {
        Pets = new(DataStore);
        Roles = new(DataStore);
        Products = new(DataStore);
        Accounts = new(DataStore);
        Employees = new(DataStore);
        Customers = new(DataStore);
        Appointments = new(DataStore);
        GroomingServices = new(DataStore);
    }

    public readonly DbSet<Pet> Pets;
    public readonly DbSet<Role> Roles;
    public readonly DbSet<Account> Accounts;
    public readonly DbSet<Product> Products;
    public readonly DbSet<Customer> Customers;
    public readonly DbSet<Employee> Employees;
    public readonly DbSet<Appointment> Appointments;
    public readonly DbSet<GroomingService> GroomingServices;
}
