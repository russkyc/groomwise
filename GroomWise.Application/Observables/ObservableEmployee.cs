// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Entities;
using Lombok.NET;

namespace GroomWise.Application.Observables;

[NotifyPropertyChanged]
public partial class ObservableEmployee
{
    [Property]
    private Guid _id;
    public string FullName => $"{FirstName} {LastName}";

    [Property]
    private string? _prefix;

    [Property]
    private string? _firstName;

    [Property]
    private string? _middleName;

    [Property]
    private string? _lastName;

    [Property]
    private string? _suffix;

    [Property]
    private string _address;

    [Property]
    private string _contactNumber;

    [Property]
    private string _email;

    [Property]
    private IList<Pet>? _pets;

    [Property]
    private IList<Appointment>? _appointments;

    [Property]
    private IList<Role>? _roles;
}
