// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Data.Migrations;

public class AccountsMigration : IDatabaseMigration
{
    public void Migrate()
    {
        BuilderServices.Resolve<IDatabaseServiceAsync>()
            .AddMultiple(
            new[]
            {
                BuilderServices.Resolve<IAccountFactory>().Create(
                    BuilderServices.Resolve<IEncryptionService>()
                        .Encrypt("russkyc"),
                    BuilderServices.Resolve<IEncryptionService>()
                        .Encrypt("russkyc@groomwise.com"),
                    "1234".SHA256(), 1),
                BuilderServices.Resolve<IAccountFactory>().Create(
                    BuilderServices.Resolve<IEncryptionService>()
                        .Encrypt("manager"),
                    BuilderServices.Resolve<IEncryptionService>()
                        .Encrypt("manager@groomwise.com"),
                    "manager".SHA256(), 2),
                BuilderServices.Resolve<IAccountFactory>().Create(
                    BuilderServices.Resolve<IEncryptionService>()
                        .Encrypt("groomer"), 
                    BuilderServices.Resolve<IEncryptionService>()
                        .Encrypt("groomer@groomwise.com"), 
                    "groomer".SHA256(), 3)
            }.ToList());
    }
}