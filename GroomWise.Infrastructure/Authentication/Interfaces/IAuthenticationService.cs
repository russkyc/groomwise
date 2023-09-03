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
    void Register(
        string username,
        string password,
        Guid? employeeId = null,
        params Role[] roles
    );

    void Register(string username, string password, params Role[] roles);
    AuthenticationStatus Login(string username, string password);

    UpdateStatus Update(
        string username,
        string password,
        string newUsername,
        string newPassword,
        params Role[] roles
    );

    AuthenticationStatus Logout();
    SessionInfo? GetSession();
}
