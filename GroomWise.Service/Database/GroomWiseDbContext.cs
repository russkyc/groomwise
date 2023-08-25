// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Domain.Entities;
using GroomWise.Service.Database.Interfaces;

namespace GroomWise.Service.Database;

public class GroomWiseDbContext : DbContext
{
    public GroomWiseDbContext(IDbStore dataStore)
        : base(dataStore)
    {
        Accounts = new(DataStore);
    }

    public readonly DbSet<Account> Accounts;
}
