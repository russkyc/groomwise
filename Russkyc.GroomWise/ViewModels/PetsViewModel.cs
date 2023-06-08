// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class PetsViewModel : ViewModelBase, IPetsViewModel
{
    private readonly ILogger _logger;
    private readonly PetFactory _petFactory;

    private readonly UnitOfWork _dbContext;

    [ObservableProperty]
    private SynchronizedObservableCollection<Pet> _petsCollection;

    public PetsViewModel(ILogger logger, PetFactory petFactory, UnitOfWork dbContext)
    {
        _logger = logger;
        _petFactory = petFactory;
        _dbContext = dbContext;

        PetsCollection = new SynchronizedObservableCollection<Pet>();

        GetPets();
    }

    void GetPets()
    {
        Task.Run(() =>
        {
            var command = new SynchronizeCollectionCommand<
                Pet,
                SynchronizedObservableCollection<Pet>
            >(ref _petsCollection, _dbContext.PetRepository.GetAll().ToList());
            command.Execute();
        });
    }
}
