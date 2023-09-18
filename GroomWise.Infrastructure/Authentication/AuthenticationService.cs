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
using NETCore.Encrypt.Extensions;
using GroomWise.Domain.Enums;

namespace GroomWise.Infrastructure.Authentication;

[RegisterSingleton<IAuthenticationService, AuthenticationService>]
public class AuthenticationService : IAuthenticationService
{
    private readonly object _lock = new();
    private readonly IEncryptionService _encryptionService;
    private readonly GroomWiseDbContext _dbContext;
    private SessionInfo? _sessionInfo;

    public AuthenticationService(GroomWiseDbContext dbContext, IEncryptionService encryptionService)
    {
        _dbContext = dbContext;
        _encryptionService = encryptionService;
    }

    public void Register(string username, string password, Role role, Guid? employeeId = null)
    {
        var account = new Account
        {
            Username = _encryptionService.Encrypt(username),
            Password = _encryptionService.Hash(password),
            Role = role
        };

        if (employeeId is null)
        {
            _dbContext.Accounts.Insert(account);
            return;
        }

        if (
            _dbContext.Employees.Get(filteredEmployee => filteredEmployee.Id.Equals(employeeId)) is
            { } employee
            
        )
        {
            account.Employee = employee;
            return;
        }

        _dbContext.Accounts.Insert(account);
    }

    public void Unregister(string username, string password)
    {
        var account = _dbContext.Accounts.Get(
            account =>
                account.Username!.Equals(_encryptionService.Encrypt(username))
                && account.Password!.Equals(password.SHA256())
        );

        if (account is null)
        {
            return;
        }

        _dbContext.Accounts.Delete(account.Id);
    }

    public AuthenticationStatus Login(string username, string password)
    {
        var account = _dbContext.Accounts.Get(
            account => account.Username!.Equals(_encryptionService.Encrypt(username))
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
            _sessionInfo = account.ToSession();
        }
        return AuthenticationStatus.Authenticated;
    }

    public UpdateStatus Update(
        string username,
        string password,
        string newUsername,
        string newPassword,
        Role role
    )
    {
        var account = _dbContext.Accounts.Get(
            account =>
                account.Username!.Equals(_encryptionService.Encrypt(username))
                && account.Password!.Equals(_encryptionService.Hash(password))
        );

        if (account is null)
        {
            return UpdateStatus.InvalidAccount;
        }

        account.Username = _encryptionService.Encrypt(newUsername);
        account.Password = _encryptionService.Hash(newPassword);
        account.Role = role;

        if (_dbContext.Accounts.Update(account.Id, account) is false)
        {
            return UpdateStatus.Fail;
        }

        return UpdateStatus.Success;
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
