// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Linq.Expressions;
using GroomWise.Application.Customers.Interfaces;
using GroomWise.Domain.Entities;
using GroomWise.Infrastructure.Data.Interfaces;

namespace GroomWise.Application.Customers;

public class CustomerController : ICustomerController
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly IRepository<Pet> _petRepository;
    private readonly IRepository<Appointment> _appointmentRepository;

    public CustomerController(IRepository<Customer> customerRepository, IRepository<Pet> petRepository, IRepository<Appointment> appointmentRepository)
    {
        _customerRepository = customerRepository;
        _petRepository = petRepository;
        _appointmentRepository = appointmentRepository;
    }

    public void Add(string firstName, string middleName, string lastName, string suffix = "")
    {
        _customerRepository.Create(
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
        var customer = _customerRepository.Search(filter);

        if (customer is not null)
        {
            customer.Pets = _petRepository.FindAll(pet => pet.Owner == customer.Id);
            customer.Appointments =
                _appointmentRepository.FindAll(appointment => appointment.Customer!.Id == customer.Id);
        }
        return customer;
    }

    public void Update(Customer? customer, Action<Customer> set)
    {
        if (customer is null)
        {
            return;
        }

        set(customer);
        _customerRepository.Update(customer);
    }

    public void Delete(Customer? customer)
    {
        if (customer is null)
        {
            return;
        }
        _customerRepository.Delete(customer.Id);
    }

    public IEnumerable<Customer>? GetAll(Expression<Func<Customer, bool>>? filter)
    {
        if (filter is null)
        {
            return _customerRepository.GetAll();
        }
        return _customerRepository.FindAll(filter);
    }
}
