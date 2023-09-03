// GroomWise
// Copyright (C) 2023  John Russell C. Camo (@russkyc)
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY

using GroomWise.Domain.Entities;
using GroomWise.Domain.Interfaces;
using LiteDB;
using Role = GroomWise.Domain.Enums.Role;

public class Account : IEntity
{
    [BsonId]
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public Employee Employee { get; set; }
    public IList<Role> Roles { get; set; } = new List<Role>();
}
