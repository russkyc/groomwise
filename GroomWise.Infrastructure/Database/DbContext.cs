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

using GroomWise.Infrastructure.Database.Interfaces;
using Injectio.Attributes;

namespace GroomWise.Infrastructure.Database;

[RegisterSingleton<DbContext>]
public abstract class DbContext
{
    protected readonly IDbStore DataStore;

    protected DbContext(IDbStore dataStore)
    {
        DataStore = dataStore;
    }
}
