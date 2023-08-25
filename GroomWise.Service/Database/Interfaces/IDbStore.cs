// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Linq.Expressions;

namespace GroomWise.Service.Database.Interfaces;

public interface IDbStore
{
    Guid Insert<T>(T entity)
        where T : class;

    int InsertMany<T>(IEnumerable<T> entities)
        where T : class;

    T? Get<T>(Expression<Func<T, bool>> filter)
        where T : class;

    IEnumerable<T> GetMultiple<T>(
        Expression<Func<T, bool>> filter,
        int skip = 0,
        int limit = Int32.MaxValue
    )
        where T : class;

    bool Update<T>(Guid id, T entity)
        where T : class;

    int UpdateMultiple<T>(Expression<Func<T, T>> extend, Expression<Func<T, bool>> filter)
        where T : class;

    bool Delete<T>(Guid id)
        where T : class;

    int DeleteMultiple<T>(Expression<Func<T, bool>> filter)
        where T : class;
}
