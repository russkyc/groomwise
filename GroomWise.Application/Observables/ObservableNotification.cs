// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Enums;
using MvvmGen;

namespace GroomWise.Application.Observables;

[ViewModel]
public partial class ObservableNotification
{
    [Property]
    private object? _icon;

    [Property]
    private string? _title;

    [Property]
    private string? _description;

    [Property]
    private NotificationType? _type;
}
