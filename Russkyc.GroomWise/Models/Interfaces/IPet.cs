// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Interfaces;

public interface IPet
{
    string? Name { get; set; }
    string? Type { get; set; }
    int? Age { get; set; }
    IEnumerable<string>? Allergies { get; set; }
    IEnumerable<IAppointment>? Appointments { get; set; }
}