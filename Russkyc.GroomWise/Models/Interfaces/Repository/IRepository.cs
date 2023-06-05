// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Interfaces.Repository;

public interface IRepository<TEntity>
{
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);

    IEnumerable<TEntity> GetAll();
    TEntity? Find(Predicate<TEntity> filter);
    IEnumerable<TEntity>? FindAll(Predicate<TEntity> filter);

    bool Remove(TEntity entity);
    int RemoveAll(Predicate<TEntity> filter);
}
