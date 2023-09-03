// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using CommunityToolkit.Mvvm.ComponentModel;
using Swordfish.NET.Collections;

namespace GroomWise.Application.Observables;

public partial class ObservableCustomer : ObservableObject
{
    [ObservableProperty]
    private Guid _id;

    [ObservableProperty]
    private string _fullName;

    [ObservableProperty]
    private string _address;

    [ObservableProperty]
    private string _contactNumber;

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private ConcurrentObservableCollection<ObservableAppointment> _appointments = new();

    [ObservableProperty]
    private ConcurrentObservableCollection<ObservablePet> _pets = new();
}
