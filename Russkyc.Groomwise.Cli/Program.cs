// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Models.Entities;
using GroomWise.Models.Interfaces.Service;
using GroomWise.Services.App;
using Russkyc.Groomwise.Cli.Services;

namespace Russkyc.Groomwise.Cli;

public class Program
{
    private static IEncryptionService _encryptionService = new EncryptionService(
        new CredentialStore()
    );

    public static void Main(string[] args)
    {
        CreateAccountsMigration();
        CreateEmployeesMigration();
    }

    static void CreateAccountsMigration()
    {
        new Migration<Account>(
            "Accounts",
            new List<Account>
            {
                new Account
                {
                    Id = 0,
                    Username = _encryptionService.Encrypt("russkyc"),
                    Password = _encryptionService.Hash("1234"),
                    Email = _encryptionService.Encrypt("russkyc@groomwise.com")
                },
                new Account
                {
                    Id = 1,
                    Username = _encryptionService.Encrypt("manager"),
                    Password = _encryptionService.Hash("manager"),
                    Email = _encryptionService.Encrypt("manager@groomwise.com")
                },
                new Account
                {
                    Id = 2,
                    Username = _encryptionService.Encrypt("groomer"),
                    Password = _encryptionService.Hash("groomer"),
                    Email = _encryptionService.Encrypt("groomer@groomwise.com")
                },
            }
        ).Create();
    }

    static void CreateEmployeesMigration()
    {
        new Migration<Employee>(
            "Employees",
            new List<Employee>
            {
                new Employee
                {
                    Id = 0,
                    FirstName = _encryptionService.Encrypt("John Russell"),
                    MiddleName = _encryptionService.Encrypt("Casabuena"),
                    LastName = _encryptionService.Encrypt("Camo")
                },
                new Employee
                {
                    Id = 1,
                    FirstName = _encryptionService.Encrypt("John"),
                    MiddleName = _encryptionService.Encrypt("Williams"),
                    LastName = _encryptionService.Encrypt("Doe")
                },
                new Employee
                {
                    Id = 2,
                    FirstName = _encryptionService.Encrypt("Karen"),
                    MiddleName = _encryptionService.Encrypt("Karen"),
                    LastName = _encryptionService.Encrypt("Karen")
                }
            }
        ).Create();
    }

    static void CreateCustomersMigration()
    {
        new Migration<Customer>(
            "Customers",
            new List<Customer>
            {
                new Customer
                {
                    Id = 0,
                    FirstName = "",
                    MiddleName = "",
                    LastName = ""
                },
                new Customer
                {
                    Id = 0,
                    FirstName = "",
                    MiddleName = "",
                    LastName = ""
                },
                new Customer
                {
                    Id = 0,
                    FirstName = "",
                    MiddleName = "",
                    LastName = ""
                }
            }
        ).Create();
    }
}
