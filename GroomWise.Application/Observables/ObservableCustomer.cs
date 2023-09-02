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
    [Property]
    private Guid _id;

    [Property]
    private string _fullName;

    [Property]
    private string _address;

    [Property]
    private string _contactNumber;

    [Property]
    private string _email;

    [Property]
    private ConcurrentObservableCollection<ObservableAppointment> _appointments = new();

    [Property]
    private ConcurrentObservableCollection<ObservablePet> _pets = new();
}
