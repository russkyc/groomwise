// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Entities;
using Lombok.NET;

namespace GroomWise.Application.Observables;

[NotifyPropertyChanged]
public partial class ObservablePet
{
    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private Guid _id;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string? _name;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private int? _age;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string? _breed;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string? _gender;

}
