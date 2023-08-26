// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Interfaces;

namespace GroomWise.Domain.Entities;

public record Product : IEntity
{
    public Guid Id { get; set; }
    public string? ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public int Quantity { get; set; }
}
