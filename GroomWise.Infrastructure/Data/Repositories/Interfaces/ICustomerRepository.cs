// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Entities;

namespace GroomWise.Infrastructure.Data.Repositories.Interfaces;

public interface ICustomerRepository
{
    void Add(Customer customer);
    void Update(Customer customer, Action<Customer> action);
    Customer? Find(Func<Customer, bool> filter);
    IEnumerable<Customer>? FindAll(Func<Customer, bool> filter);
    bool Remove(Customer customer);
    int RemoveAll(Predicate<Customer> filter);
}