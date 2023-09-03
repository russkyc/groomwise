// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using CommunityToolkit.Mvvm.ComponentModel;
using GroomWise.Domain.Enums;
using Swordfish.NET.Collections;

namespace GroomWise.Application.Observables;

public partial class ObservableAppointment : ObservableObject
{
    [ObservableProperty]
    private Guid _id;

    [ObservableProperty]
    private DateTime _date;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TimeSpan))]
    private TimeOnly _startTime;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TimeSpan))]
    private TimeOnly _endTime;

    public string TimeSpan => $"{StartTime} - {EndTime}";

    [ObservableProperty]
    private AppointmentStatus? _status;

    [ObservableProperty]
    private ObservablePet? _pet;

    [ObservableProperty]
    private ObservableCustomer _customer;

    [ObservableProperty]
    private ConcurrentObservableCollection<ObservableEmployee>? _employees = new();

    [ObservableProperty]
    private ConcurrentObservableCollection<ObservableAppointmentService>? _services = new();
}
