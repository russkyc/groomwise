// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Collections.ObjectModel;
using GroomWise.Application.Mappers;
using GroomWise.Application.Observables;
using GroomWise.Domain.Entities;
using GroomWise.Infrastructure.Database;
using GroomWise.Infrastructure.Logging.Interfaces;
using GroomWise.Infrastructure.Storage.Interfaces;
using MvvmGen;

namespace GroomWise.Application.ViewModels;

[ViewModel(typeof(Customer))]
[ViewModelGenerateInterface]
[Inject(typeof(ILogger))]
[Inject(typeof(IFileStorage))]
[Inject(typeof(GroomWiseDbContext))]
public partial class CustomerViewModel
{
    [Property]
    private ObservableCollection<ObservableCustomer> _customers;

    partial void OnInitialize()
    {
        PopulateCollections();
    }

    private void PopulateCollections()
    {
        Customers = new ObservableCollection<ObservableCustomer>(
            GroomWiseDbContext!.Customers
                .GetAll()
                .Select(customer => CustomerMapper.Instance.CustomerToObservable(customer))
        );
    }
}
