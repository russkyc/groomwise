// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using CommunityToolkit.Mvvm.ComponentModel;
using GroomWise.Domain.Enums;

namespace GroomWise.Application.Observables;

public partial class ObservableNotification : ObservableObject
{
    [ObservableProperty]
    private object? _icon;

    [ObservableProperty]
    private string? _title;

    [ObservableProperty]
    private string? _description;

    [ObservableProperty]
    private NotificationType? _type;
}
