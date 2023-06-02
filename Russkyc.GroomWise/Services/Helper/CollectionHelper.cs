// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Helper;

public static class CollectionHelper
{
    public static void SyncTo<T, TTarget>(
        this ICollection<T> sourceCollection,
        ref TTarget targetCollection
    )
        where T : IEntity
        where TTarget : SynchronizedObservableCollection<T>
    {
        var target = targetCollection;
        var source = sourceCollection;
        targetCollection.AddRange(
            sourceCollection.Where(
                entity => target.FirstOrDefault(item => item.Id == entity.Id) is null
            )
        );
        var range = target.Except(source);
        targetCollection.RemoveRange(range);
    }
}
