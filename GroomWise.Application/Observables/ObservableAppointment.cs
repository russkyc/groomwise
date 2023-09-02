// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Enums;
using Lombok.NET;
using Swordfish.NET.Collections;

namespace GroomWise.Application.Observables;

[NotifyPropertyChanged]
public partial class ObservableAppointment
{
    [Property]
    private Guid _id;

    [Property]
    private DateOnly? _date;

    [Property]
    private TimeOnly? _startTime;

    [Property]
    private TimeOnly? _endTime;

    [Property]
    private AppointmentStatus? _status;

    [Property]
    private ObservablePet? _pet;

    [Property]
    private ObservableCustomer? _customer;

    [Property]
    private ConcurrentObservableCollection<ObservableEmployee>? _employees = new();

    [Property]
    private ConcurrentObservableCollection<ObservableAppointmentService>? _services = new();
}
