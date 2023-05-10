// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Interfaces.Entity;

public interface IAccount : IEntity
{
    string? Username { get; set; }
    string? Email { get; set; }
    string? Password { get; set; }
    int? EmployeeId { get; set; }
}