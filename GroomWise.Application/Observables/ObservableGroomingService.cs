// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using CommunityToolkit.Mvvm.ComponentModel;

namespace GroomWise.Application.Observables;

public partial class ObservableGroomingService : ObservableObject
{
    [ObservableProperty]
    private Guid _id;

    [ObservableProperty]
    private string? _type;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TimeSpan))]
    private double _hourSpan;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TimeSpan))]
    private double _minuteSpan;

    public TimeSpan TimeSpan =>
        new TimeSpan().Add(TimeSpan.FromHours(HourSpan)).Add(TimeSpan.FromMinutes(MinuteSpan));

    [ObservableProperty]
    private string? _description;
}
