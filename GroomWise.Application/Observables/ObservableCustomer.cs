// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Collections.ObjectModel;
using GroomWise.Domain.Entities;
using Lombok.NET;

namespace GroomWise.Application.Observables;

[NotifyPropertyChanged]
public partial class ObservableCustomer
{
    public string FullName => $"{FirstName} {MiddleName[0]}. {LastName}";

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string _firstName;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string _middleName;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string _lastName;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string _contactNumber;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string _email;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private string _address;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private ObservableCollection<Appointment> _appointments;

    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private ObservableCollection<Pet> _pets;
}
