// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Factory;

public class EmployeeFactory : IEmployeeFactory
{
    public Employee Create()
    {
        return new Employee();
    }

    public Employee Create(params object[] values)
    {
        return new Employee
        {
            FirstName = (string)values[0],
            MiddleName = (string)values[1],
            LastName = (string)values[2],
            AddressId = (int)values[3],
            EmployeeType = (int)values[4]
        };
    }
}