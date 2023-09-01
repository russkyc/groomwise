// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Interfaces;
using LiteDB;

namespace GroomWise.Domain.Entities;

public class GroomingService : IEntity
{
    [BsonId]
    public Guid Id { get; set; }
    public string? Type { get; set; }
    public int? HourSpan { get; set; }
    public int? MinuteSpan { get; set; }
    public string? Description { get; set; }
}
