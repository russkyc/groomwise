// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Linq.Expressions;

namespace GroomWise.Infrastructure.Database.Interfaces;

public interface IDbSet<T>
    where T : class
{
    Guid Insert(T entity);
    int InsertMany(IEnumerable<T> entities);
    T? Get(Expression<Func<T, bool>> filter);

    IEnumerable<T> GetMultiple(
        Expression<Func<T, bool>> filter,
        int skip = 0,
        int limit = Int32.MaxValue
    );

    bool Update(Guid id, T entity);
    int UpdateMultiple(Expression<Func<T, T>> extend, Expression<Func<T, bool>> filter);
    bool Delete(Guid id);
    int DeleteMultiple(Expression<Func<T, bool>> filter);
    IEnumerable<T> GetAll();
}
