﻿// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Entities;
using Lombok.NET;

namespace GroomWise.Application.Observables;

[NotifyPropertyChanged]
public partial class ObservableEmployee
{
    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private Guid _id;
    public string FullName => $"{FirstName} {LastName}";

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string? _prefix;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string? _firstName;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string? _middleName;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string? _lastName;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string? _suffix;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string _address;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string _contactNumber;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string _email;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private IList<Pet>? _pets;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private IList<Appointment>? _appointments;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private IList<Role>? _roles;
}
