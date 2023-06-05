// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Repository;

public class GroomingServiceRepository : IGroomingServiceRepository
{
    private readonly IDatabaseServiceAsync _databaseService;

    public GroomingServiceRepository(IDatabaseServiceAsync databaseService)
    {
        _databaseService = databaseService;
    }

    public bool Add(GroomingService item)
    {
        return Task.Run(async () => await _databaseService.Add(item)).Result;
    }

    public bool AddMultiple(ICollection<GroomingService> items)
    {
        return Task.Run(async () => await _databaseService.AddMultiple(items)).Result;
    }

    public GroomingService Get(Expression<Func<GroomingService, bool>> filter)
    {
        return Task.Run(async () => await _databaseService.Get(filter)).Result;
    }

    public ICollection<GroomingService> GetMultiple(Expression<Func<GroomingService, bool>> filter)
    {
        return Task.Run(async () => await _databaseService.GetMultiple(filter)).Result;
    }

    public ICollection<GroomingService> GetCollection()
    {
        return Task.Run(async () => await _databaseService.GetCollection<GroomingService>()).Result;
    }

    public bool Update(
        Expression<Func<GroomingService, bool>> filter,
        Expression<Func<GroomingService, GroomingService>> action
    )
    {
        return Task.Run(async () => await _databaseService.Update(filter, action)).Result;
    }

    public bool Delete(Expression<Func<GroomingService, bool>> filter)
    {
        return Task.Run(async () => await _databaseService.Delete(filter)).Result;
    }
}
