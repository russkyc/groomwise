// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Entities;
using GroomWise.Domain.Interfaces;
using LiteDB;
using Role = GroomWise.Domain.Enums.Role;

public class Account : IEntity
{
    [BsonId]
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public Employee Employee { get; set; }
    public IList<Role> Roles { get; set; } = new List<Role>();
}
