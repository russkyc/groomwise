// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Infrastructure.Database.Interfaces;

namespace GroomWise.Infrastructure.Database;

public abstract class DbContext
{
    protected readonly IDbStore DataStore;

    protected DbContext(IDbStore dataStore)
    {
        DataStore = dataStore;
    }
}
