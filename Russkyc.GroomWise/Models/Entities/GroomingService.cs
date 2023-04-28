// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Models.Interfaces;

namespace GroomWise.Models.Entities;

public class GroomingService : IGroomingService
{
    public string ServiceId { get; set; }
    public string ServiceName { get; set; }
    public string ServiceDescription { get; set; }
    public bool ServiceAvailability { get; set; }
    public int ServicePrice { get; set; }
}