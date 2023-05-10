// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Entities;

public class Appointment : IAppointment
{
    [Column(IsIdentity = true, IsPrimary = true)]
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? Date { get; set; }
    public int? PetId { get; set; }
    public int? CustomerId { get; set; }
    public int? EmployeeId { get; set; }
    public int? GroomingServiceId { get; set; }
    public int? AppointmentStatus { get; set; }
}