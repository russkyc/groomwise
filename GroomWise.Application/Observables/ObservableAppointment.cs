// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Entities;
using GroomWise.Domain.Enums;
using Lombok.NET;

namespace GroomWise.Application.Observables;

[NotifyPropertyChanged]
public partial class ObservableAppointment
{
    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private DateTime? _date;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private AppointmentStatus? _status;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private GroomingService? _service;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private Pet? _pet;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private Customer? _customer;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private IEnumerable<Employee>? _employees;
}
