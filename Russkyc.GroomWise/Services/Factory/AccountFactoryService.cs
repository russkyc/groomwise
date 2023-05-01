// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Factory;

public class AccountFactoryService : IAccountFactoryService
{
    public IAccount Create()
    {
        return new Account();
    }

    public IAccount Create(params object[] values)
    {
        return new Account
        {
            FirstName = (string)values[0],
            MiddleName = (string)values[1],
            LastName = (string)values[2],
            Email = new []{(string)values[3]},
            Username = (string)values[4],
            Password = (string)values[5]
        };
    }
}