// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Repository;

public class PetsRepository : IPetRepository
{
    private readonly IDatabaseServiceAsync _databaseService;

    public PetsRepository(IDatabaseServiceAsync databaseService)
    {
        _databaseService = databaseService;
    }

    public bool Add(Pet item)
    {
        return Task.Run(async () => await _databaseService.Add(item)).Result;
    }

    public bool AddMultiple(ICollection<Pet> items)
    {
        return Task.Run(async () => await _databaseService.AddMultiple(items)).Result;
    }

    public Pet Get(Expression<Func<Pet, bool>> filter)
    {
        return Task.Run(async () => await _databaseService.Get(filter)).Result;
    }

    public ICollection<Pet> GetMultiple(Expression<Func<Pet, bool>> filter)
    {
        return Task.Run(async () => await _databaseService.GetMultiple(filter)).Result;
    }

    public ICollection<Pet> GetCollection()
    {
        return Task.Run(async () => await _databaseService.GetCollection<Pet>()).Result;
    }

    public bool Update(Expression<Func<Pet, bool>> filter, Expression<Func<Pet, Pet>> action)
    {
        return Task.Run(async () => await _databaseService.Update(filter, action)).Result;
    }

    public bool Delete(Expression<Func<Pet, bool>> filter)
    {
        return Task.Run(async () => await _databaseService.Delete(filter)).Result;
    }
}
