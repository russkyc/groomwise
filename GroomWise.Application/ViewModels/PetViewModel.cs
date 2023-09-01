// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Application.Events;
using GroomWise.Application.Mappers;
using GroomWise.Application.Observables;
using GroomWise.Infrastructure.Database;
using GroomWise.Infrastructure.Navigation.Interfaces;
using Injectio.Attributes;
using MvvmGen;
using MvvmGen.Events;
using Swordfish.NET.Collections;
using Swordfish.NET.Collections.Auxiliary;

namespace GroomWise.Application.ViewModels;

[ViewModel]
[RegisterSingleton]
[Inject(typeof(GroomWiseDbContext))]
[Inject(typeof(IDialogService))]
[Inject(typeof(INavigationService))]
[Inject(typeof(IEventAggregator))]
public partial class PetViewModel
    : IEventSubscriber<CreateCustomerEvent>,
        IEventSubscriber<DeleteCustomerEvent>
{
    [Property]
    private ObservablePet _activePet = new();

    [Property]
    private ConcurrentObservableCollection<ObservablePet> _pets = new();

    partial void OnInitialize()
    {
        PopulateCollections();
    }

    private async void PopulateCollections()
    {
        await Task.Run(() =>
        {
            Pets = new();
            var customers = GroomWiseDbContext.Customers
                .GetAll()
                .Select(CustomerMapper.ToObservable);
            customers.ForEach(customer => customer.Pets.ForEach(Pets.Add));
        });
    }

    public void OnEvent(CreateCustomerEvent eventData)
    {
        PopulateCollections();
    }

    public void OnEvent(DeleteCustomerEvent eventData)
    {
        PopulateCollections();
    }
}
