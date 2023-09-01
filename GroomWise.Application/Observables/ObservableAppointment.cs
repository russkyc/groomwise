// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Entities;
using GroomWise.Domain.Enums;
using Lombok.NET;
using Swordfish.NET.Collections;

namespace GroomWise.Application.Observables;

[NotifyPropertyChanged]
public partial class ObservableAppointment
{
    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private DateTime? _date;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private AppointmentStatus? _status;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private ObservablePet? _pet;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private ObservableCustomer? _customer;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private ConcurrentObservableCollection<ObservableEmployee>? _employees = new();

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private ConcurrentObservableCollection<ObservableAppointmentService>? _services = new();
}
