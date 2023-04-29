// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Repository;

public class AccountsRepositoryService : IAccountsRepositoryService
{
    private IDatabaseService _databaseService;

    public AccountsRepositoryService(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public bool Insert(IAccount item)
    {
        return _databaseService.Insert(item);
    }

    public bool Insert(ICollection<IAccount> item)
    {
        return _databaseService.Insert(item);
    }

    public IAccount Get(Func<IAccount, bool> filter)
    {
        return _databaseService.Get(filter);
    }

    public ICollection<IAccount> GetAll(Func<IAccount, bool> filter)
    {
        return _databaseService.GetAll(filter);
    }

    public ICollection<IAccount> GetCollection()
    {
        return _databaseService.GetCollection<IAccount>();
    }

    public bool Update(Func<IAccount, bool> filter, Action<IAccount> action)
    {
        return _databaseService.Update(filter, action);
    }

    public bool Delete(Func<IAccount, bool> filter)
    {
        return _databaseService.Delete(filter);
    }
}