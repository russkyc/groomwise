// GroomWise
// Copyright (C) 2023  John Russell C. Camo (@russkyc)
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY

using System.Linq.Expressions;

namespace GroomWise.Infrastructure.Database.Interfaces;

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

    IEnumerable<T> GetAll<T>();
}
