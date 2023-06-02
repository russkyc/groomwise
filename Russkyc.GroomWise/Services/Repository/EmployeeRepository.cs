// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Repository;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IEncryptionService _encryptionService;
    private IDatabaseServiceAsync _databaseService;

    public EmployeeRepository(
        IDatabaseServiceAsync databaseService,
        IEncryptionService encryptionService
    )
    {
        _databaseService = databaseService;
        _encryptionService = encryptionService;
    }

    public bool Add(Employee item)
    {
        return Task.Run(async () => await _databaseService.Add(item)).Result;
    }

    public bool AddMultiple(ICollection<Employee> items)
    {
        return Task.Run(async () => await _databaseService.AddMultiple(items)).Result;
    }

    public Employee Get(Expression<Func<Employee, bool>> filter)
    {
        return Task.Run(async () => await _databaseService.Get(filter)).Result;
    }

    public ICollection<Employee> GetMultiple(Expression<Func<Employee, bool>> filter)
    {
        return Task.Run(async () => await _databaseService.GetMultiple(filter)).Result;
    }

    public ICollection<Employee> GetCollection()
    {
        return Task.Run(async () => await _databaseService.GetCollection<Employee>()).Result;
    }

    public bool Update(
        Expression<Func<Employee, bool>> filter,
        Expression<Func<Employee, Employee>> action
    )
    {
        return Task.Run(async () => await _databaseService.Update(filter, action)).Result;
    }

    public bool Delete(Expression<Func<Employee, bool>> filter)
    {
        return Task.Run(async () => await _databaseService.Delete(filter)).Result;
    }
}
