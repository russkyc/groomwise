// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Interfaces;
using LiteDB;

namespace GroomWise.Domain.Entities;

public class Role : IEntity
{
    [BsonId]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}
