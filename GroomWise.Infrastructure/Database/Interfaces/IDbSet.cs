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
