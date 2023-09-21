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
using GroomWise.Domain.Entities;

namespace GroomWise.Application.Mappers;

public static class GroomingServiceMapper
{
    public static GroomingService ToEntity(this ObservableGroomingService o)
    {
        return new GroomingService
        {
            Id = o.Id,
            Type = o.Type,
            Description = o.Description,
            Duration = o.Duration
        };
    }

    public static ObservableGroomingService ToObservable(this GroomingService g)
    {
        return new ObservableGroomingService
        {
            Id = g.Id,
            Type = g.Type,
            Description = g.Description,
            Duration = g.Duration
        };
    }
}
