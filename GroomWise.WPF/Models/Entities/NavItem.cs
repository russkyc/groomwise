// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Entities;

public record NavItem : IEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ShortName { get; set; }
    public string? TooltipDescription { get; set; }
    public Type? Page { get; set; }
    public bool Selected { get; set; }
    public object? Icon { get; set; }
    public int[]? Roles { get; set; }
}
