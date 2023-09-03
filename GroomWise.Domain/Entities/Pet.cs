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

using GroomWise.Domain.Interfaces;
using LiteDB;

namespace GroomWise.Domain.Entities;

public class Pet : IEntity
{
    [BsonId]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int? Age { get; set; }
    public string? Breed { get; set; }
    public string? Gender { get; set; }
}
