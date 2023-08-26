// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Enums;
using Lombok.NET;

namespace GroomWise.Application.Observables;

[NotifyPropertyChanged]
public partial class ObservableNotification
{
    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private object? _icon;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string? _title;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string? _description;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private NotificationType? _type;
}
