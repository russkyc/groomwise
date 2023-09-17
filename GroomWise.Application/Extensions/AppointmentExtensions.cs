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

using GroomWise.Application.Observables;
using Swordfish.NET.Collections;

namespace GroomWise.Application.Extensions;

public static class AppointmentExtensions
{
    public static bool IsNullOrEmpty(
        this ConcurrentObservableCollection<ObservableAppointmentService>? collection
    ) => collection is null || collection.Count == 0 || collection.Any(service => service is null);
}
