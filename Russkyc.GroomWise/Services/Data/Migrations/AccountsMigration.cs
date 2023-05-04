// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Data.Migrations;

public class AccountsMigration : IDatabaseMigration
{
    public void Migrate()
    {
        BuilderServices.Resolve<IDatabaseService>().AddMultiple(
            new[]
            {
                BuilderServices.Resolve<IAccountFactoryService>().Create(
                    "John Russell",
                    "Casabuena",
                    "Camo",
                    "russkyc@groomwise.com",
                    "russkyc",
                    "tcu700600",
                    AccountType.Admin),
                BuilderServices.Resolve<IAccountFactoryService>().Create(
                    "John Doe",
                    "",
                    "",
                    "groomer@groomwise.com",
                    "groomer",
                    "groomer",
                    AccountType.Groomer),
                BuilderServices.Resolve<IAccountFactoryService>().Create(
                    "Karen",
                    "",
                    "",
                    "customer@groomwise.com",
                    "customer",
                    "customer",
                    AccountType.Groomer)
            }.ToList());
    }
}