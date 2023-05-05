﻿// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Interfaces.Entity;

public interface IAppointment
{
    DateTime? Date { get; set; }
    TimeSpan? Time { get; set; }
    IEnumerable<IGroomingService>? Services { get; set; }
    IEnumerable<IPet>? Pet { get; set; }
    ICustomer? Customer { get; set; }
    IEnumerable<IGroomer>? Groomers { get; set; }
    AppointmentStatus? Status { get; set; }
}