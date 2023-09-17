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

using GroomWise.Application.Events;
using GroomWise.Application.Extensions;
using GroomWise.Application.Mappers;
using GroomWise.Application.Observables;
using GroomWise.Infrastructure.Database;
using GroomWise.Infrastructure.Navigation.Interfaces;
using Injectio.Attributes;
using MvvmGen;
using MvvmGen.Events;
using Swordfish.NET.Collections;

namespace GroomWise.Application.ViewModels;

[ViewModel]
[RegisterSingleton]
[Inject(typeof(GroomWiseDbContext))]
[Inject(typeof(IDialogService))]
[Inject(typeof(INavigationService))]
[Inject(typeof(IEventAggregator))]
public partial class PetViewModel
    : IEventSubscriber<CreateCustomerEvent>,
        IEventSubscriber<DeleteCustomerEvent>,
        IEventSubscriber<UpdateCustomerEvent>
{
    [Property]
    private ObservablePet _activePet = new();

    [Property]
    private ConcurrentObservableCollection<ObservablePet> _pets = new();

    partial void OnInitialize()
    {
        PopulateCollections();
    }

    private void PopulateCollections()
    {
        Pets = GroomWiseDbContext.Customers
            .GetAll()
            .Select(CustomerMapper.ToObservable)
            .SelectMany(customer => customer.Pets)
            .AsObservableCollection();
    }

    public void OnEvent(CreateCustomerEvent eventData)
    {
        PopulateCollections();
    }

    public void OnEvent(DeleteCustomerEvent eventData)
    {
        PopulateCollections();
    }

    public void OnEvent(UpdateCustomerEvent eventData)
    {
        PopulateCollections();
    }
}
