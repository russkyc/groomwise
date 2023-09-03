// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using CommunityToolkit.Mvvm.ComponentModel;

namespace GroomWise.Application.Observables;

[ObservableObject]
public partial class TimeGridItem
{
    [ObservableProperty]
    private TimeOnly _time;

    [ObservableProperty]
    private bool _available;

    public TimeGridItem(TimeOnly time, bool available)
    {
        Time = time;
        Available = available;
    }
}
