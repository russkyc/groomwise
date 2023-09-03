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
using Mapster;

namespace GroomWise.Application.Mappers;

public static class PetMapper
{
    public static Pet ToEntity(this ObservablePet observablePet)
    {
        return observablePet.Adapt<Pet>();
    }

    public static ObservablePet ToObservable(this Pet pet)
    {
        return pet.Adapt<ObservablePet>();
    }
}
