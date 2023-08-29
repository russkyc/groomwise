// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Collections.ObjectModel;
using GroomWise.Application.Events;
using GroomWise.Application.Mappers;
using GroomWise.Application.Observables;
using GroomWise.Domain.Enums;
using GroomWise.Infrastructure.Database;
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
[Inject(typeof(GroomWiseDbContext))]
[RegisterSingleton]
public partial class CustomerViewModel
{
    [Property]
    private ObservableCustomer _activeCustomer;

    [Property]
    private ConcurrentObservableCollection<ObservableCustomer> _customers;

    partial void OnInitialize()
    {
        ActiveCustomer = new ObservableCustomer();
        PopulateCollections();
    }

    private async void PopulateCollections()
    {
        await Task.Run(() =>
        {
            var customers = GroomWiseDbContext!.Customers
                .GetAll()
                .Select(CustomerMapper.ToObservable);
            Customers = new ConcurrentObservableCollection<ObservableCustomer>(customers);
        });
    }

    [Command]
    private async Task CreateCustomer()
    {
        await Task.Run(() =>
        {
            DialogService.CreateAddCustomersDialog(this, NavigationService);
        });
    }

    [Command]
    private async Task SaveCustomer()
    {
        await Task.Run(() =>
        {
            var dialogResult = DialogService.Create(
                "GroomWise",
                "Save Customer?",
                NavigationService
            );
            if (dialogResult is true)
            {
                if (string.IsNullOrEmpty(ActiveCustomer.FullName))
                {
                    EventAggregator.Publish(
                        new PublishNotificationEvent(
                            "Customer name cannot be empty.",
                            NotificationType.Danger
                        )
                    );
                    return;
                }
                var customer = ActiveCustomer.ToEntity();
                GroomWiseDbContext.Customers.Insert(customer);
                DialogService.CloseDialogs(NavigationService);
                EventAggregator.Publish(
                    new PublishNotificationEvent(
                        $"Customer {ActiveCustomer.FullName} added.",
                        NotificationType.Success
                    )
                );
                ActiveCustomer = new ObservableCustomer();
                PopulateCollections();
            }
        });
    }

    [Command]
    private async Task UpdateCustomer(object param)
    {
        if (param is ObservableCustomer observableCustomer)
        {
            await Task.Run(
                () =>
                    GroomWiseDbContext.Customers.Update(
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
            await Task.Run(() => GroomWiseDbContext.Customers.Delete(observableCustomer.Id));
        }
    }

    [Command]
    private async Task CloseDialogs()
    {
        await Task.Run(() =>
        {
            DialogService.CloseDialogs(NavigationService);
        });
    }
}
