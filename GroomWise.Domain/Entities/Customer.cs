// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Interfaces;
using LiteDB;

namespace GroomWise.Domain.Entities;

public class Customer : IEntity
{
    public Guid Id { get; set; }

    public string? Prefix { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? Suffix { get; set; }
    public string? Address { get; set; }
    public string? ContactNumber { get; set; }
    public string? Email { get; set; }

    [BsonRef("pets")]
    public IList<Pet>? Pets { get; set; }

    [BsonRef("appointments")]
    public IList<Appointment>? Appointments { get; set; }
}
