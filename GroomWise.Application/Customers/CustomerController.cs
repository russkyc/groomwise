// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Linq.Expressions;
using GroomWise.Application.Customers.Interfaces;
using GroomWise.Domain.Entities;
using GroomWise.Domain.Extensions;
using GroomWise.Infrastructure.Data.Interfaces;

namespace GroomWise.Application.Customers;

public class CustomerController : ICustomerController
{
    private readonly IRepository<Customer> _repository;

    public CustomerController(IRepository<Customer> repository)
    {
        _repository = repository;
    }

    public void Add(string firstName, string middleName, string lastName, string suffix = "")
    {
        _repository.Create(
            new Customer
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Suffix = suffix
            }
        );
    }

    public Customer? Find(Expression<Func<Customer, bool>> filter)
    {
        return _repository.Search(filter);
    }

    public void Update(Customer? customer, Action<Customer> set)
    {
        if (customer is null)
            return;
        set(customer);
        _repository.Update(customer);
    }

    public void Delete(Customer? customer)
    {
        if (customer is null)
            return;
        _repository.Delete(customer.Id);
    }

    public IEnumerable<Customer>? GetAll(Expression<Func<Customer, bool>>? filter)
    {
        if (filter is null)
            return _repository.GetAll();
        return _repository.FindAll(filter);
    }
}
