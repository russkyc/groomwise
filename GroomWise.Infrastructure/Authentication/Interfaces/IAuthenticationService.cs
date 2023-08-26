// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Infrastructure.Authentication.Enums;
using GroomWise.Infrastructure.Session.Entities;

namespace GroomWise.Infrastructure.Authentication.Interfaces;

public interface IAuthenticationService
{
    void Register(string username, string password, Guid employeeId);
    AuthenticationStatus Login(string username, string password);

    string Update(
        string username,
        string password,
        string newUsername = "",
        string newPassword = ""
    );

    string Logout();
    SessionInfo? GetSession();
}
