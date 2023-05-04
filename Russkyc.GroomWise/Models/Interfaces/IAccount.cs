// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Interfaces;

public interface IAccount : IPerson
{
    string? PhoneNumber { get; set; }
    string? Email { get; set; }
    string? Username { get; set; }
    string? Password { get; set; }
    AccountType? Type { get; set; }
}