// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Repository;

public class EmployeeRepository : Repository<Employee>
{
    private readonly IEncryptionService _encryptionService;

    public EmployeeRepository(
        IDatabaseServiceAsync databaseService,
        IEncryptionService encryptionService
    )
        : base(databaseService)
    {
        _encryptionService = encryptionService;
    }

    public new IEnumerable<Employee> GetAll()
    {
        return base.GetAll().Select(employee => _encryptionService.Decrypt(employee));
    }
}
