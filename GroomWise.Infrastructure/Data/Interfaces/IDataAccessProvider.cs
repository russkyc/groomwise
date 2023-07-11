// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Linq.Expressions;

namespace GroomWise.Infrastructure.Data.Interfaces;

public interface IDataAccessProvider
{
    Guid Add<T>(T entity);
    T Get<T>(Guid id);
    IEnumerable<T>? GetAll<T>();
    int AddMany<T>(IEnumerable<T> collection);
    bool Update<T>(T entity);
    int UpdateMany<T>(Expression<Func<T, T>> action, Expression<Func<T, bool>> filter);
    bool Delete<T>(Guid id);
    int DeleteMany<T>(Expression<Func<T, bool>> filter);
    T? Find<T>(Expression<Func<T, bool>> filter);
    IEnumerable<T> FindMany<T>(
        Expression<Func<T, bool>> filter,
        int skip = 0,
        int limit = Int32.MaxValue
    );
}
