// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Repository;

public class AccountsRepository : IAccountsRepository
{

    private readonly IDatabaseServiceAsync _databaseService;

    public AccountsRepository(IDatabaseServiceAsync databaseService)
    {
        _databaseService = databaseService;
    }

    public bool Add(Account item)
    {
        return Task.Run(
            async () => await _databaseService.Add(item)
            ).Result;
    }

    public bool AddMultiple(ICollection<Account> items)
    {
        return Task.Run(
            async () => await _databaseService.AddMultiple(items)
            ).Result;
    }

    public Account Get(Expression<Func<Account, bool>> filter)
    {
        return Task.Run(
            async () => await _databaseService.Get(filter)
            ).Result;
    }

    public ICollection<Account> GetMultiple(Expression<Func<Account, bool>> filter)
    {
        return Task.Run(
            async () => await _databaseService.GetMultiple(filter)
            ).Result;
    }

    public ICollection<Account> GetCollection()
    {
        return Task.Run(
            async () => await _databaseService.GetCollection<Account>()
            ).Result;
    }

    public bool Update(Expression<Func<Account, bool>> filter, Expression<Func<Account, Account>> action)
    {
        return Task.Run(
            async () => await _databaseService.Update(filter, action)
            ).Result;
    }

    public bool Delete(Expression<Func<Account, bool>> filter)
    {
        return Task.Run(
            async () => await _databaseService.Delete(filter)
            ).Result;
    }
}