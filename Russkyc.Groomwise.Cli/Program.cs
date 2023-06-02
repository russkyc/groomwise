// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Models.Entities;
using Russkyc.Groomwise.Cli.Services;

namespace Russkyc.Groomwise.Cli;

public class Program
{
    public static void Main(string[] args)
    {
        CreateAccountsMigration();
        CreateEmployeesMigration();
    }

    static void CreateAccountsMigration()
    {
        new Migration<Account>(
            "Accounts.json",
            "Accounts",
            new List<Account>
            {
                new Account
                {
                    Id = 0,
                    Username = "russkyc",
                    Password = "1234",
                    Email = "russkyc@groomwise.com",
                    EmployeeId = 0
                },
                new Account
                {
                    Id = 1,
                    Username = "manager",
                    Password = "manager",
                    Email = "manager@groomwise.com",
                    EmployeeId = 1
                },
                new Account
                {
                    Id = 2,
                    Username = "groomer",
                    Password = "groomer",
                    Email = "groomer@groomwise.com",
                    EmployeeId = 2
                },
            }
        ).Create();
    }

    static void CreateEmployeesMigration()
    {
        new Migration<Employee>(
            "Employees.json",
            "Employees",
            new List<Employee>
            {
                new Employee
                {
                    Id = 0,
                    FirstName = "John Russell",
                    MiddleName = "Casabuena",
                    LastName = "Camo",
                    AddressId = 0,
                    EmployeeType = 0
                },
                new Employee
                {
                    Id = 1,
                    FirstName = "John",
                    MiddleName = "Williams",
                    LastName = "Doe",
                    AddressId = 1,
                    EmployeeType = 1
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Karen",
                    MiddleName = "Karen",
                    LastName = "Karen",
                    AddressId = 2,
                    EmployeeType = 2
                }
            }
        ).Create();
    }
}
