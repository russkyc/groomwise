// GroomWise
// Copyright (C) 2023  John Russell C. Camo (@russkyc)
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY

using GroomWise.Infrastructure.Authentication.Entities;
using GroomWise.Infrastructure.Authentication.Enums;
using GroomWise.Infrastructure.Authentication.Interfaces;
using GroomWise.Infrastructure.Authentication.Mappers;
using GroomWise.Infrastructure.Database;
using GroomWise.Infrastructure.Encryption.Interfaces;
using Injectio.Attributes;
using Role = GroomWise.Domain.Enums.Role;

namespace GroomWise.Infrastructure.Authentication;

[RegisterSingleton<IAuthenticationService, AuthenticationService>]
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
        Guid? employeeId = null,
        params Role[] roles
    )
    {
        var account = new Account();
        account.Username = _encryptionService.Hash(username);
        account.Password = _encryptionService.Hash(password);

        foreach (var role in roles)
        {
            account.Roles.Add(role);
        }

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

    public void Register(string username, string password, params Role[] roles)
    {
        Register(username, password, null, roles);
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
        string newPassword,
        params Role[] roles
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

        foreach (var role in roles)
        {
            account.Roles.Add(role);
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
