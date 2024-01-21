﻿// GroomWise
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

namespace GroomWise.Application.Observables;

public partial class ObservableEmployee : ObservableObject
{
    [ObservableProperty] private string? _address;

    [ObservableProperty] private string? _contactNumber;

    [ObservableProperty] private string? _email;

    [ObservableProperty] private string? _fullName;

    [ObservableProperty] private Guid _id;

    [ObservableProperty] private string? _prefix;

    [ObservableProperty] private string? _suffix;
}