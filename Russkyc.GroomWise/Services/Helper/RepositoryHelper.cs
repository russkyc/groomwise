// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Helper;

public static class RepositoryHelper
{
    public static int GetLastId<T>(this Repository<T> repository)
        where T : class, IEquatable<T>, IEntity, new()
    {
        return repository.GetAll().OrderByDescending(saved => saved.Id).First().Id;
    }

    public static int CountAll<T>(this Repository<T> repository)
        where T : class, IEquatable<T>, IEntity, new()
    {
        return repository.GetAll().Count();
    }

    public static int CountWhere<T>(this Repository<T> repository, Predicate<T> filter)
        where T : class, IEquatable<T>, IEntity, new()
    {
        return repository.FindAll(filter).Count();
    }
}
