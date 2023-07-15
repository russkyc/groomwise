﻿// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Interfaces;

namespace GroomWise.Domain.Entities;

public record Address : IEntity
{
    public Guid Id { get; set; }
    public string? PrimaryAddress { get; set; }
    public string? SecondaryAddress { get; set; }
}