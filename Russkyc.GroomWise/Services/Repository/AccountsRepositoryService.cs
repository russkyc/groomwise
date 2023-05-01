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

    public bool Add(IAccount item)
    {
        return _databaseService.Add(item);
    }

    public bool AddMultiple(ICollection<IAccount> items)
    {
        return _databaseService.AddMultiple(items);
    }

    public IAccount Get(Func<IAccount, bool> filter)
    {
        return _databaseService.Get(filter);
    }

    public ICollection<IAccount> GetMultiple(Func<IAccount, bool> filter)
    {
        return _databaseService.GetMultiple(filter);
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

    public bool Contains(Func<IAccount, bool> filter)
    {
        return _databaseService.Contains<IAccount>(filter);
    }
}