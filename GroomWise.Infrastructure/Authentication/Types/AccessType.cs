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

namespace GroomWise.Infrastructure.Authentication.Types;

public static class AccessType
{
    public static readonly IEnumerable<Role> All = new[] { Role.Admin, Role.Manager, Role.Groomer };
    public static readonly IEnumerable<Role> AdminOnly = new[] { Role.Admin, Role.Manager };
    public static readonly IEnumerable<Role> EmployeeOnly = new[] { Role.Groomer };
}
