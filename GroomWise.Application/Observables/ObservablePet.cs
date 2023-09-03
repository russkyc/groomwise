// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using CommunityToolkit.Mvvm.ComponentModel;

namespace GroomWise.Application.Observables;

public partial class ObservablePet : ObservableObject
{
    [ObservableProperty]
    private Guid _id;

    [ObservableProperty]
    private string? _name;

    [ObservableProperty]
    private int? _age;

    [ObservableProperty]
    private string? _breed;

    [ObservableProperty]
    private string? _gender;
}
