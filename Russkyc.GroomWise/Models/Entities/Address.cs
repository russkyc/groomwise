// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Entities;

public class Address : IEntity
{
    [Column(IsIdentity = true, IsPrimary = true)]
    public int Id { get; set; }
    public string? Province { get; set; }
    public string? City { get; set; }
    public string? Barangay { get; set; }
    public string? HouseNumber { get; set; }
    public string? ZipCode { get; set; }
}
