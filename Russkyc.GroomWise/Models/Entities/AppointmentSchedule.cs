// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Entities;

public class AppointmentSchedule : IAppointmentSchedule
{
    public string? Day { get; set; }
    public string? Time { get; set; }
    public string? Date { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}