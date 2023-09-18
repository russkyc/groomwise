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

using GroomWise.Domain.Enums;
using GroomWise.Infrastructure.Authentication.Entities;
using GroomWise.Infrastructure.Authentication.Enums;

namespace GroomWise.Infrastructure.Authentication.Interfaces;

public interface IAuthenticationService
{
    void Register(string username, string password, Role role, Guid? employeeId = null);
    void Unregister(string username, string password);
    AuthenticationStatus Login(string username, string password);

    UpdateStatus Update(
        string username,
        string password,
        string newUsername,
        string newPassword,
        Role role
    );

    AuthenticationStatus Logout();
    SessionInfo? GetSession();
}
