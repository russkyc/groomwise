﻿// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace Russkyc.GroomWise.Models.Interfaces;

public interface IAddress
{
    string HouseNumber { get; set; }
    string Street { get; set; }
    string Barangay { get; set; }
    string City { get; set; }
    string Province { get; set; }
    string ZipCode { get; set; }
}