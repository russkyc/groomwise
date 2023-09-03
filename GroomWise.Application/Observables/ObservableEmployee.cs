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
using GroomWise.Domain.Entities;
using Swordfish.NET.Collections;

namespace GroomWise.Application.Observables;

public partial class ObservableEmployee : ObservableObject
{
    [ObservableProperty]
    private Guid _id;
    public string FullName => $"{FirstName} {LastName}";

    [ObservableProperty]
    private string? _prefix;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string? _firstName;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string? _middleName;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string? _lastName;

    [ObservableProperty]
    private string? _suffix;

    [ObservableProperty]
    private string _address;

    [ObservableProperty]
    private string _contactNumber;

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private ConcurrentObservableCollection<ObservablePet>? _pets = new();

    [ObservableProperty]
    private ConcurrentObservableCollection<ObservableAppointment>? _appointments = new();

    [ObservableProperty]
    private ConcurrentObservableCollection<Role>? _roles = new();
}
