// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Repository;

public class EmployeeRepository : IEmployeeRepository
{

    private IDatabaseServiceAsync _databaseService;

    public EmployeeRepository(IDatabaseServiceAsync databaseService)
    {
        _databaseService = databaseService;
    }

    public bool Add(Employee item)
    {
        return Task.Run(
            async () => await _databaseService.Add(item)
        ).GetAwaiter().GetResult();
    }

    public bool AddMultiple(ICollection<Employee> items)
    {
        return Task.Run(
            async () => await _databaseService.AddMultiple(items)
        ).GetAwaiter().GetResult();
    }

    public Employee Get(Expression<Func<Employee, bool>> filter)
    {
        return Task.Run(
            async () => await _databaseService.Get(filter)
        ).GetAwaiter().GetResult();
    }

    public ICollection<Employee> GetMultiple(Expression<Func<Employee, bool>> filter)
    {
        return Task.Run(
            async () => await _databaseService.GetMultiple(filter)
        ).GetAwaiter().GetResult();
    }

    public ICollection<Employee> GetCollection()
    {
        return Task.Run(
            async () => await _databaseService.GetCollection<Employee>()
        ).GetAwaiter().GetResult();
    }

    public bool Update(Expression<Func<Employee, bool>> filter, Expression<Func<Employee, Employee>> action)
    {
        return Task.Run(
            async () => await _databaseService.Update(filter, action)
        ).GetAwaiter().GetResult();
    }

    public bool Delete(Expression<Func<Employee, bool>> filter)
    {
        return Task.Run(
            async () => await _databaseService.Delete(filter)
        ).GetAwaiter().GetResult();
    }
}