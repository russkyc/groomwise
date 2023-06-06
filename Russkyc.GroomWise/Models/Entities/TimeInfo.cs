// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Entities;

public record TimeInfo
{
    public string? Time { get; init; }
    public int AmHour { get; init; }
    public int PmHour { get; init; }
    public int Minutes { get; init; }
    public bool IsAm { get; set; }
}
