// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Data.Migrations;

public class EmployeesMigration : IDatabaseMigration
{

    public void Migrate()
    {
        BuilderServices.Resolve<IDatabaseServiceAsync>()
            .AddMultiple(
                new[]
                {
                    BuilderServices.Resolve<IEncryptionService>()
                        .Encrypt(BuilderServices.Resolve<IEmployeeFactory>()
                            .Create("John Russell", 
                                "Casabuena", 
                                "Camo", 0, 1)),
                    BuilderServices.Resolve<IEncryptionService>()
                        .Encrypt(BuilderServices.Resolve<IEmployeeFactory>()
                            .Create("John Russell", 
                                "Casabuena", 
                                "Camo", 0, 1)),
                    BuilderServices.Resolve<IEncryptionService>()
                        .Encrypt(BuilderServices.Resolve<IEmployeeFactory>()
                            .Create("John Russell", 
                                "Casabuena", 
                                "Camo", 0, 2))
                }.ToList());
    }
}