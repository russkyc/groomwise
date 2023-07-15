﻿// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Entities;

namespace GroomWise.Application.Customers.Interfaces;

public interface ICustomerController
{
    void Add(string firstName, string middleName, string lastName, string suffix = "");
    Customer? FindByName(
        string firstName = "",
        string middleName = "",
        string lastName = "",
        string suffix = ""
    );
    void Update(Customer? customer, Action<Customer> set);
    void Delete(Customer? customer);
    IEnumerable<Customer>? GetAll();
}
