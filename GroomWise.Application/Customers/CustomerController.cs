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

    public Customer? FindByName(
        string firstName = "",
        string middleName = "",
        string lastName = "",
        string suffix = ""
    )
    {
        return _repository.Search(
            customer =>
                (firstName.IsNullOrEmpty() || customer.FirstName!.Equals(firstName))
                && (middleName.IsNullOrEmpty() || customer.MiddleName!.Equals(middleName))
                && (lastName.IsNullOrEmpty() || customer.LastName!.Equals(lastName))
                && (suffix.IsNullOrEmpty() || customer.Suffix!.Equals(suffix))
        );
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

    public IEnumerable<Customer>? GetAll()
    {
        return _repository.GetAll();
    }

    public IEnumerable<Customer>? GetAll(Expression<Func<Customer, bool>> filter)
    {
        return _repository.FindAll(filter);
    }
}
