// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Enums;
using GroomWise.Domain.Interfaces;
using LiteDB;

namespace GroomWise.Domain.Entities;

public record Appointment : IEntity
{
    public Guid Id { get; set; }
    public DateTime? Date { get; set; }
    public AppointmentStatus? Status { get; set; }

    [BsonRef("groomingServices")]
    public GroomingService? Service { get; set; }

    [BsonRef("pets")]
    public Pet? Pet { get; set; }

    [BsonRef("customers")]
    public Customer? Customer { get; set; }

    [BsonRef("employees")]
    public IList<Employee>? Employees { get; set; }
}
