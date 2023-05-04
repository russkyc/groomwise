// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Entities;

public class Appointment : IAppointment
{
    public DateTime? Date { get; set; }
    public TimeSpan? Time { get; set; }
    public IEnumerable<IGroomingService>? Services { get; set; }
    public IEnumerable<IPet>? Pet { get; set; }
    public ICustomer? Customer { get; set; }
    public IEnumerable<IGroomer>? Groomers { get; set; }
    public AppointmentStatus? Status { get; set; }
}