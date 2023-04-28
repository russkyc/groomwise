// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Models.Interfaces;

namespace GroomWise.Services;

public class LiteDbDataService : IDatabaseService
{
    public bool Insert<T>(T item)
    {
        throw new NotImplementedException();
    }

    public bool Insert<T>(ICollection<T> item)
    {
        throw new NotImplementedException();
    }

    public T Get<T>(Func<T, bool> filter)
    {
        throw new NotImplementedException();
    }

    public ICollection<T> GetAll<T>(Func<T, bool> filter)
    {
        throw new NotImplementedException();
    }

    public ICollection<T> GetCollection<T>()
    {
        throw new NotImplementedException();
    }

    public bool Update<T>(Func<T, bool> filter, Action<T> action)
    {
        throw new NotImplementedException();
    }

    public bool Delete<T>(Func<T, bool> filter)
    {
        throw new NotImplementedException();
    }
}