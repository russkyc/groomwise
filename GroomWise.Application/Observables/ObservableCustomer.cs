// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Entities;
using Lombok.NET;
using Swordfish.NET.Collections;

namespace GroomWise.Application.Observables;

[NotifyPropertyChanged]
public partial class ObservableCustomer
{
    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private Guid _id;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string _fullName;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string _address;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string _contactNumber;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string _email;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private ConcurrentObservableCollection<Appointment> _appointments = new();

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private ConcurrentObservableCollection<Pet> _pets = new();
}
