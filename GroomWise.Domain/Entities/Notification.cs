// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Enums;
using GroomWise.Domain.Interfaces;

namespace GroomWise.Domain.Entities;

public record Notification : IEntity
{
    public Guid Id { get; set; }
    public object? Icon { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public NotificationType? Type { get; set; }
}
