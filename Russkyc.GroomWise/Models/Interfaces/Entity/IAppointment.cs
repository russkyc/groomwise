// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Interfaces.Entity;

public interface IAppointment : IEntity
{
    string? Title { get; set; }
    string? Description { get; set; }
    DateTime? Date { get; set; }
    int? PetId { get; set; }
    int? CustomerId { get; set; }
    int? EmployeeId { get; set; }
    int? GroomingServiceId { get; set; }
    int? AppointmentStatus { get; set; }
}