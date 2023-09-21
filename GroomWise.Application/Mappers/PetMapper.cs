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

public static class PetMapper
{
    public static Pet ToEntity(this ObservablePet o)
    {
        return new Pet
        {
            Name = o.Name,
            Age = o.Age,
            Breed = o.Breed,
            Gender = o.Gender
        };
    }

    public static ObservablePet ToObservable(this Pet p)
    {
        return new ObservablePet
        {
            Id = p.Id,
            Name = p.Name,
            Age = p.Age,
            Breed = p.Breed,
            Gender = p.Gender
        };
    }
}
