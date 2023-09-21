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

public static class EmployeeMapper
{
    public static Employee ToEntity(this ObservableEmployee o)
    {
        return new Employee
        {
            FullName = o.FullName,
            Prefix = o.Prefix,
            Suffix = o.Suffix,
            Address = o.Address,
            ContactNumber = o.ContactNumber,
            Email = o.Email
        };
    }

    public static ObservableEmployee ToObservable(this Employee e)
    {
        return new ObservableEmployee
        {
            Id = e.Id,
            FullName = e.FullName,
            Prefix = e.Prefix,
            Suffix = e.Suffix,
            Address = e.Address,
            ContactNumber = e.ContactNumber,
            Email = e.Email
        };
    }
}
