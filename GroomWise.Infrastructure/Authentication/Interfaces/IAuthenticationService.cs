// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

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
