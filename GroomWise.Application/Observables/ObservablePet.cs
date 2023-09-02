// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Entities;
using Lombok.NET;

namespace GroomWise.Application.Observables;

[NotifyPropertyChanged]
public partial class ObservablePet
{
    [Property]
    private Guid _id;

    [Property]
    private string? _name;

    [Property]
    private int? _age;

    [Property]
    private string? _breed;

    [Property]
    private string? _gender;

}
