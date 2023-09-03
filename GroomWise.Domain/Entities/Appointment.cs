// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Enums;
using GroomWise.Domain.Interfaces;
using LiteDB;

namespace GroomWise.Domain.Entities;

public class Appointment : IEntity
{
    [BsonId]
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public AppointmentStatus? Status { get; set; }
    public Pet? Pet { get; set; }
    public Customer Customer { get; set; }
    public IList<GroomingService>? Services { get; set; }
    public IList<Employee>? Employees { get; set; }
}
