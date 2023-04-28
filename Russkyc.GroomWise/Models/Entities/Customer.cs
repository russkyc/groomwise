// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Entities;

public class Customer : ICustomer
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public IEnumerable<string> PhoneNumber { get; set; }
    public IEnumerable<string> Email { get; set; }
    public IAddress Address { get; set; }
    public IEnumerable<IPet> Pets { get; set; }
}