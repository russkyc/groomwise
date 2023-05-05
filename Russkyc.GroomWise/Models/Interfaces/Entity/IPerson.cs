﻿// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Interfaces.Entity;

public interface IPerson
{
    string? FirstName { get; set; }
    string? MiddleName { get; set; }
    string? LastName { get; set; }
    IAddress? Address { get; set; }
}