// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using Lombok.NET;

namespace GroomWise.Application.Observables;

[NotifyPropertyChanged]
public partial class ObservableGroomingService
{
    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private Guid _id;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string? _type;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private TimeSpan? _timeSpan;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string? _description;
}
