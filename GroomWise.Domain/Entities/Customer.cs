// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Interfaces;
using LiteDB;

namespace GroomWise.Domain.Entities;

public class Customer : IEntity
{
    [BsonId]
    public Guid Id { get; set; }
    public string? FullName { get; set; }
    public string? Address { get; set; }
    public string? ContactNumber { get; set; }
    public string? Email { get; set; }
    public IList<Pet>? Pets { get; set; }
    public IList<Appointment>? Appointments { get; set; }
}
