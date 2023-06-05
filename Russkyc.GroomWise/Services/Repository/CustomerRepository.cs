// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly IDatabaseServiceAsync _databaseService;

    public CustomerRepository(IDatabaseServiceAsync databaseService)
    {
        _databaseService = databaseService;
    }

    public bool Add(Customer item)
    {
        return Task.Run(async () => await _databaseService.Add(item)).Result;
    }

    public bool AddMultiple(ICollection<Customer> items)
    {
        return Task.Run(async () => await _databaseService.AddMultiple(items)).Result;
    }

    public Customer Get(Expression<Func<Customer, bool>> filter)
    {
        return Task.Run(async () => await _databaseService.Get(filter)).Result;
    }

    public ICollection<Customer> GetMultiple(Expression<Func<Customer, bool>> filter)
    {
        return Task.Run(async () => await _databaseService.GetMultiple(filter)).Result;
    }

    public ICollection<Customer> GetCollection()
    {
        return Task.Run(async () => await _databaseService.GetCollection<Customer>()).Result;
    }

    public bool Update(
        Expression<Func<Customer, bool>> filter,
        Expression<Func<Customer, Customer>> action
    )
    {
        return Task.Run(async () => await _databaseService.Update(filter, action)).Result;
    }

    public bool Delete(Expression<Func<Customer, bool>> filter)
    {
        return Task.Run(async () => await _databaseService.Delete(filter)).Result;
    }
}