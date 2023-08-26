// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Entities;
using GroomWise.Infrastructure.Authentication.Enums;
using GroomWise.Infrastructure.Authentication.Interfaces;
using GroomWise.Infrastructure.Authentication.Mappers;
using GroomWise.Infrastructure.Database;
using GroomWise.Infrastructure.Encryption.Interfaces;
using GroomWise.Infrastructure.Session.Entities;
using Russkyc.DependencyInjection.Attributes;
using Russkyc.DependencyInjection.Enums;

namespace GroomWise.Infrastructure.Authentication;

[Service(Scope.Singleton, Registration.AsSelfAndInterfaces)]
public class AuthenticationService : IAuthenticationService
{
    private object _lock = new();
    private SessionInfo? _sessionInfo = null;
    private readonly IEncryptionService _encryptionService;
    private readonly GroomWiseDbContext _dbContext;

    public AuthenticationService(GroomWiseDbContext dbContext, IEncryptionService encryptionService)
    {
        _dbContext = dbContext;
        _encryptionService = encryptionService;
    }

    public void Register(string username, string password, Guid employeeId)
    {
        var employee = _dbContext.Employees.Get(employee => employee.Id.Equals(employeeId));

        if (employee is null)
        {
            return;
        }

        var account = new Account
        {
            Username = _encryptionService.Hash(username),
            Password = _encryptionService.Hash(password),
            Employee = employee
        };

        _dbContext.Accounts.Insert(account);
    }

    public AuthenticationStatus Login(string username, string password)
    {
        var account = _dbContext.Accounts.Get(
            account => account.Username!.Equals(_encryptionService.Hash(username))
        );

        if (account is null)
        {
            return AuthenticationStatus.InvalidAccount;
        }

        if (!account.Password!.Equals(_encryptionService.Hash(password)))
        {
            return AuthenticationStatus.InvalidPassword;
        }

        lock (_lock)
        {
            _sessionInfo = account.Employee.ToSession();
        }
        return AuthenticationStatus.Authenticated;
    }

    public string Update(
        string username,
        string password,
        string newUsername = "",
        string newPassword = ""
    )
    {
        var account = _dbContext.Accounts.Get(
            account =>
                account.Username!.Equals(_encryptionService.Hash(username))
                && account.Password!.Equals(_encryptionService.Hash(password))
        );

        if (account is null)
        {
            return "Account does not exist.";
        }

        if (!string.IsNullOrEmpty(username))
        {
            account.Username = _encryptionService.Hash(newUsername);
        }

        if (!string.IsNullOrEmpty(password))
        {
            account.Password = _encryptionService.Hash(newPassword);
        }

        _dbContext.Accounts.Update(account.Id, account);

        return "Account successfully updated";
    }

    public string Logout()
    {
        lock (_lock)
        {
            _sessionInfo = null!;
        }
        return "Logout successful.";
    }

    public SessionInfo? GetSession()
    {
        lock (_lock)
        {
            return _sessionInfo;
        }
    }
}
