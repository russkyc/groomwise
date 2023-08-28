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
using Role = GroomWise.Domain.Enums.Role;

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

    public void Register(
        string username,
        string password,
        Role role = Role.User,
        Guid? employeeId = null
    )
    {
        var account = new Account();
        account.Username = _encryptionService.Hash(username);
        account.Password = _encryptionService.Hash(password);
        account.Roles.Add(role);

        if (employeeId is not null)
        {
            var employee = _dbContext.Employees.Get(employee => employee.Id.Equals(employeeId));

            if (employee is not null)
            {
                account.Employee = employee;
            }
        }

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

    public UpdateStatus Update(
        string username,
        string password,
        string newUsername,
        string newPassword
    )
    {
        var account = _dbContext.Accounts.Get(
            account =>
                account.Username!.Equals(_encryptionService.Hash(username))
                && account.Password!.Equals(_encryptionService.Hash(password))
        );

        if (account is null)
        {
            return UpdateStatus.InvalidAccount;
        }

        if (!string.IsNullOrEmpty(username))
        {
            account.Username = _encryptionService.Hash(newUsername);
        }

        if (!string.IsNullOrEmpty(password))
        {
            account.Password = _encryptionService.Hash(newPassword);
        }

        bool executeUpdate = _dbContext.Accounts.Update(account.Id, account);

        if (executeUpdate)
        {
            return UpdateStatus.Success;
        }

        return UpdateStatus.Fail;
    }

    public AuthenticationStatus Logout()
    {
        lock (_lock)
        {
            _sessionInfo = null!;
        }
        return AuthenticationStatus.NotAuthenticated;
    }

    public SessionInfo? GetSession()
    {
        lock (_lock)
        {
            return _sessionInfo;
        }
    }
}
