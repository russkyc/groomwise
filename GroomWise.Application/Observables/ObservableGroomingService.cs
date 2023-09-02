// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using MvvmGen;

namespace GroomWise.Application.Observables;

[ViewModel]
public partial class ObservableGroomingService
{
    [Property]
    private Guid _id;

    [Property]
    private string? _type;

    [Property]
    private double _hourSpan;

    [Property]
    private double _minuteSpan;

    [PropertyInvalidate(nameof(HourSpan), nameof(MinuteSpan))]
    public TimeSpan TimeSpan =>
        new TimeSpan().Add(TimeSpan.FromHours(HourSpan)).Add(TimeSpan.FromMinutes(MinuteSpan));

    [Lombok.NET.Property]
    private string? _description;
}
