// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Factory;

public class AccountFactory : IAccountFactory
{
    public Account Create()
    {
        return new Account();
    }

    public Account Create(params object[] values)
    {
        return new Account
        {
            Username = (string)values[0],
            Email = (string)values[1],
            Password = (string)values[2],
            EmployeeId = (int)values[3]
        };
    }
}