// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

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
