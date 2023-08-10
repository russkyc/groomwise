﻿// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Interfaces;

namespace GroomWise.Domain.Entities;

public record Customer : IEntity
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? Suffix { get; set; }
    public string? ContactNumber { get; set; }
    public string? Telephone { get; set; }
    public string? Email { get; set; }
    public string? PrimaryAddress { get; set; }
    public string? SecondaryAddress { get; set; }
    public IEnumerable<Pet>? Pets { get; set; }
    public IEnumerable<Appointment>? Appointments { get; set; }
}
