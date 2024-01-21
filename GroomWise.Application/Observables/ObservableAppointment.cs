// GroomWise
// Copyright (C) 2023  John Russell C. Camo (@russkyc)
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY

using CommunityToolkit.Mvvm.ComponentModel;
using GroomWise.Domain.Enums;
using Swordfish.NET.Collections;

namespace GroomWise.Application.Observables;

public partial class ObservableAppointment : ObservableObject
{
    [ObservableProperty] private ObservableCustomer _customer;

    [ObservableProperty] private DateTime _date;

    [ObservableProperty] private ConcurrentObservableCollection<ObservableEmployee>? _employees = new();

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(TimeSpan))]
    private TimeOnly _endTime;

    [ObservableProperty] private Guid _id;

    [ObservableProperty] private ObservablePet? _pet;

    [ObservableProperty] private ConcurrentObservableCollection<ObservableAppointmentService>? _services = new();

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(TimeSpan))]
    private TimeOnly _startTime;

    [ObservableProperty] private AppointmentStatus? _status;

    public string TimeSpan => $"{StartTime} - {EndTime}";
}