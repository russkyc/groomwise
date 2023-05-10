// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Interfaces.Entity;

public interface INavItem : IEntity
{
    string? Name { get; set; }
    string? ShortName { get; set; }
    string? TooltipDescription { get; set; }
    Type? Page { get; set; }
    bool Selected { get; set; }
    object? Icon { get; set; }
    EmployeeType[]? AccountTypes { get; set; }
}