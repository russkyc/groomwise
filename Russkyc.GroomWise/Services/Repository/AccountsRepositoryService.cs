// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Repository;

public class AccountsRepositoryService : IAccountsRepositoryService
{
    private readonly IDatabaseService _databaseService;

    public AccountsRepositoryService(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public bool Add(Account item)
    {
        return _databaseService.Add(item);
    }

    public bool AddMultiple(ICollection<Account> items)
    {
        return _databaseService.AddMultiple(items);
    }

    public Account Get(Expression<Func<Account, bool>> filter)
    {
        return _databaseService.Get(filter);
    }

    public ICollection<Account> GetMultiple(Expression<Func<Account, bool>> filter)
    {
        return _databaseService.GetMultiple(filter);
    }

    public ICollection<Account> GetCollection()
    {
        return _databaseService.GetCollection<Account>();
    }

    public bool Update(Expression<Func<Account, bool>> filter, Expression<Func<Account, Account>> action)
    {
        return _databaseService.Update(filter, action);
    }

    public bool Delete(Expression<Func<Account, bool>> filter)
    {
        return _databaseService.Delete(filter);
    }
}