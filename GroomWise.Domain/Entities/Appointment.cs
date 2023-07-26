// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Interfaces;

namespace GroomWise.Domain.Entities;

public record Appointment : IEntity
{
    public Guid Id { get; set; }
    public DateTime? Date { get; set; }
    public int? Status { get; set; }
    public Pet? Pet { get; set; }
    public Customer? Customer { get; set; }
    public GroomingService? Service { get; set; }
    public IEnumerable<Employee>? Employees { get; set; }
}
