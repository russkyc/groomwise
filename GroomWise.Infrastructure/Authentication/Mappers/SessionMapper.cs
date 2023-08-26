// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Entities;
using GroomWise.Infrastructure.Session.Entities;
using Lombok.NET;
using Riok.Mapperly.Abstractions;

namespace GroomWise.Infrastructure.Authentication.Mappers;

[Mapper]
[Singleton]
public partial class SessionMapper
{
    public partial SessionInfo EmployeeToSession(Employee employee);
}
