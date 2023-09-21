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

namespace GroomWise.Application.Mappers;

public static class AccountMapper
{
    public static Account ToEntity(this ObservableAccount o)
    {
        return new Account
        {
            Username = o.Username,
            Password = o.Password,
            Role = o.Role,
            Employee = o.Employee?.ToEntity()
        };
    }

    public static ObservableAccount ToObservable(this Account a)
    {
        return new ObservableAccount
        {
            Id = a.Id,
            Username = a.Username,
            Password = a.Password,
            Role = a.Role,
            Employee = a.Employee?.ToObservable()
        };
    }
}
