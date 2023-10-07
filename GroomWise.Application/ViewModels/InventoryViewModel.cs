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

using GroomWise.Application.Extensions;
using GroomWise.Application.Mappers;
using GroomWise.Application.Observables;
using GroomWise.Infrastructure.Database;
using GroomWise.Infrastructure.IoC.Interfaces;
using GroomWise.Infrastructure.Logging.Interfaces;
using GroomWise.Infrastructure.Navigation.Interfaces;
using GroomWise.Infrastructure.Storage.Interfaces;
using Injectio.Attributes;
using MvvmGen;
using MvvmGen.Events;
using Swordfish.NET.Collections;

namespace GroomWise.Application.ViewModels;

[ViewModel]
[ViewModelGenerateInterface]
[Inject(typeof(ILogger))]
[Inject(typeof(IFileStorage))]
[Inject(typeof(IEventAggregator))]
[Inject(typeof(IDialogService))]
[Inject(typeof(INavigationService))]
[Inject(typeof(IAppServicesContainer))]
[Inject(typeof(GroomWiseDbContext))]
[RegisterSingleton]
public partial class InventoryViewModel
{
    [Property]
    private ConcurrentObservableCollection<ObservableProduct> _products = new();

    partial void OnInitialize()
    {
        PopulateCollections();
    }

    private void PopulateCollections()
    {
        Products = GroomWiseDbContext.Products
            .GetAll()
            .Select(product => product.ToObservable())
            .AsObservableCollection();
    }
}
