// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Collections.ObjectModel;
using GroomWise.Application.Mappers;
using GroomWise.Application.Observables;
using GroomWise.Infrastructure.Database;
using GroomWise.Infrastructure.Logging.Interfaces;
using GroomWise.Infrastructure.Storage.Interfaces;
using Injectio.Attributes;
using MvvmGen;

namespace GroomWise.Application.ViewModels;

[ViewModel]
[ViewModelGenerateInterface]
[Inject(typeof(ILogger))]
[Inject(typeof(IFileStorage))]
[Inject(typeof(GroomWiseDbContext))]
[RegisterSingleton]
public partial class CustomerViewModel
{
    [Property]
    private ObservableCustomer _activeCustomer;

    [Property]
    private ObservableCollection<ObservableCustomer> _customers;

    partial void OnInitialize()
    {
        // PopulateCollections();
    }

    private void PopulateCollections()
    {
        var customers = GroomWiseDbContext!.Customers.GetAll().Select(CustomerMapper.ToObservable);
        Customers = new ObservableCollection<ObservableCustomer>(customers);
    }

    private async Task CreateCustomer()
    {
        ActiveCustomer = new();
        await Task.CompletedTask;
    }

    [Command]
    private async Task SaveCustomer()
    {
        await Task.Run(() => GroomWiseDbContext!.Customers.Insert(ActiveCustomer.ToEntity()));
        await CreateCustomer();
    }

    [Command]
    private async Task UpdateCustomer(object param)
    {
        if (param is ObservableCustomer observableCustomer)
        {
            await Task.Run(
                () =>
                    GroomWiseDbContext!.Customers.Update(
                        observableCustomer.Id,
                        observableCustomer.ToEntity()
                    )
            );
        }
    }

    [Command]
    private async Task DeleteCustomer(object param)
    {
        if (param is ObservableCustomer observableCustomer)
        {
            await Task.Run(() => GroomWiseDbContext!.Customers.Delete(observableCustomer.Id));
        }
    }
}
