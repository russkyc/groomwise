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
using Mapster;

namespace GroomWise.Application.Mappers;

public static class AccountMapper
{
    public static Account ToEntity(this ObservableAccount observableAccount)
    {
        return observableAccount.Adapt<Account>();
    }

    public static ObservableAccount ToObservable(this Account account)
    {
        var config = TypeAdapterConfig<Account, ObservableAccount>.NewConfig();
        if (account.Employee is not null)
        {
            config.Map(dest => dest.Employee, src => src.Employee.ToObservable());
        }
        return account.Adapt<ObservableAccount>();
    }
}
