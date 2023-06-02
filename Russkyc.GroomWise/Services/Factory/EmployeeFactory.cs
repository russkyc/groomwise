// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Factory;

public class EmployeeFactory : IEmployeeFactory
{
    public Employee Create(Action<Employee> builder = null)
    {
        var employee = new Employee();
        builder(employee);
        return employee;
    }
}
