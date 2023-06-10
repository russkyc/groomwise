// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Commands;

public class SynchronizeCollectionCommand<T, TCollection> : Interfaces.ICommand
    where TCollection : SynchronizedObservableCollection<T>
    where T : IEntity
{
    public bool CanExecute { get; private set; }
    private readonly TCollection _target;
    private readonly ICollection<T> _source;

    public SynchronizeCollectionCommand(ref TCollection target, ICollection<T> source)
    {
        _target = target;
        _source = source;
        CanExecute = !source.IsEmpty();
    }

    public void Execute()
    {
        if (CanExecute)
        {
            // remove items from _target that are not in _source
            var itemsToRemove = _target
                .Where(targetItem => !_source.Any(sourceItem => sourceItem.Id == targetItem.Id))
                .ToList();
            foreach (var itemToRemove in itemsToRemove)
            {
                _target.Remove(itemToRemove);
            }

            // update items in _target with corresponding items from _source
            foreach (var sourceItem in _source)
            {
                var targetItem = _target.FirstOrDefault(t => t.Id == sourceItem.Id);
                if (targetItem != null)
                {
                    // update existing item
                    _target.Remove(targetItem);
                    _target.Add(sourceItem);
                }
                else
                {
                    // add new item
                    _target.Add(sourceItem);
                }
            }

            // add new items from _source that don't exist in _target
            var itemsToAdd = _source
                .Where(sourceItem => !_target.Any(targetItem => targetItem.Id == sourceItem.Id))
                .ToList();
            foreach (var itemToAdd in itemsToAdd)
            {
                _target.Add(itemToAdd);
            }
        }
    }
}
