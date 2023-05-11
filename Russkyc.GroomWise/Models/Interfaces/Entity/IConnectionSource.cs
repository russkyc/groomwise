// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Interfaces.Entity;

public interface IConnectionSource
{
    string? Path { get; set; }
    string? Port { get; set; }
    string? Database { get; set; }
    string? Username { get; set; }
    string? Password { get; set; }
}
