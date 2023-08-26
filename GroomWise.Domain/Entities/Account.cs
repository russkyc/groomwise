// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Interfaces;
using LiteDB;

namespace GroomWise.Domain.Entities;

public class Account : IEntity
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public Employee Employee { get; set; }
}
