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
        CanExecute = true;
    }

    public void Execute()
    {
        if (CanExecute)
        {
            _target.RemoveRange(_target.Except(_source).ToList());
            _target.AddRange(
                _source
                    .Where(
                        sourceItem => !(_target.Any(targetItem => targetItem.Id == sourceItem.Id))
                    )
                    .ToList()
            );
        }
    }
}
