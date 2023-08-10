// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Entities;
using GroomWise.Infrastructure.Data.Interfaces;
using GroomWise.Infrastructure.Data.Repositories.Interfaces;

namespace GroomWise.Infrastructure.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IDataAccessProvider _dbAccess;

    public CustomerRepository(IDataAccessProvider dbAccess)
    {
        _dbAccess = dbAccess;
    }

    public void Add(Customer customer)
    {
        _dbAccess.Add(customer);
    }

    public void Update(Customer customer, Action<Customer> action)
    {
        action(customer);
        _dbAccess.Update(customer);
    }

    public Customer? Find(Func<Customer, bool> filter)
    {
        return _dbAccess.Find<Customer>(t => filter(t));
    }

    public IEnumerable<Customer>? FindAll(Func<Customer, bool> filter)
    {
        return _dbAccess.FindMany<Customer>(t => filter(t));
    }

    public bool Remove(Customer customer)
    {
        return _dbAccess.Delete<Customer>(customer.Id);
    }

    public int RemoveAll(Predicate<Customer> filter)
    {
        return _dbAccess.DeleteMany<Customer>(t => filter(t));
    }
}