// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Entities;

public class Pet : IPet
{
    [Column(IsIdentity = true, IsPrimary = true)]
    public int Id { get; set; }
    public int? Age { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Allergies { get; set; }
    public int? AppointmentId { get; set; }
}
