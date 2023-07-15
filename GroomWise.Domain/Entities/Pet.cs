// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Interfaces;

namespace GroomWise.Domain.Entities;

public record Pet : IEntity
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Breed { get; set; }
    public int? Age { get; set; }
    public int? Gender { get; set; }
}
