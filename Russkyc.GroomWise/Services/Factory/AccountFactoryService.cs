// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Factory;

public class AccountFactoryService : IAccountFactoryService
{
    private readonly IEncryptionService _encryptionService;

    public AccountFactoryService(IEncryptionService encryptionService)
    {
        _encryptionService = encryptionService;

    }

    public Account Create()
    {
        return new Account();
    }

    public Account Create(params object[] values)
    {
        return _encryptionService.Hash(
            new Account
            {
                FirstName = (string)values[0],
                MiddleName = (string)values[1],
                LastName = (string)values[2],
                Email = (string)values[3],
                Username = (string)values[4],
                Password = (string)values[5],
                Type = (AccountType)values[6]
            },
            "FirstName",
            "MiddleName",
            "LastName");
    }
}