// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Abstractions;

/// <summary>
/// A thread-safe dynamic data collection based on <see cref="ObservableCollection{T}"/>
/// </summary>
/// <typeparam name="T">The type of elements in the collection</typeparam>
public abstract class SynchronizedObservableCollection<T> : ObservableCollection<T>
{
    protected SynchronizedObservableCollection()
    {
        // Enable synchronization
        BindingOperations.EnableCollectionSynchronization(this, new object());
    }
    
}