﻿// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Interfaces;

public interface IGroomingService
{
    string ServiceId { get; set; }
    string ServiceName { get; set; }
    string ServiceDescription { get; set; }
    bool ServiceAvailability { get; set; }
    int ServicePrice { get; set; }
}