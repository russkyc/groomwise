// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

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
    private ObservableCustomer _selectedCustomer;

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
                .Select(CustomerMapper.ToObservable)
                .OrderBy(customer => customer.FullName);
            Customers = new ConcurrentObservableCollection<ObservableCustomer>(customers);
        });
    }

    [Command]
    private async Task CreateCustomer()
    {
        await Task.Run(() =>
        {
            ActiveCustomer = new();
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
                EventAggregator.Publish(new CreateCustomerEvent());
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
    private async Task SelectCustomer(object param)
    {
        await Task.Run(() =>
        {
            if (param is ObservableCustomer customer)
            {
                SelectedCustomer = customer;
            }
        });
    }

    [Command]
    private async Task AddCustomerPet()
    {
        ActiveCustomer.Pets.Insert(0, new ObservablePet());
    }

    [Command]
    private async Task AddSelectedCustomerPet()
    {
        SelectedCustomer.Pets.Insert(0, new ObservablePet());
    }

    [Command]
    private async Task RemoveCustomerPet(object param)
    {
        if (param is ObservablePet pet)
        {
            ActiveCustomer.Pets.Remove(pet);
        }
    }

    [Command]
    private async Task RemoveSelectedCustomerPet(object param)
    {
        if (param is ObservablePet pet)
        {
            SelectedCustomer.Pets.Remove(pet);
            GroomWiseDbContext.Customers.Update(SelectedCustomer.Id, SelectedCustomer.ToEntity());
        }
    }

    [Command]
    private async Task UpdateCustomer()
    {
        await Task.Run(() =>
        {
            var dialogResult = DialogService.Create(
                "GroomWise",
                $"Update {SelectedCustomer.FullName}?",
                NavigationService
            );
            if (dialogResult is true)
            {
                GroomWiseDbContext.Customers.Update(
                    SelectedCustomer.Id,
                    SelectedCustomer.ToEntity()
                );
                DialogService.CloseDialogs(NavigationService);
            }
        });
    }

    [Command]
    private async Task EditCustomer()
    {
        DialogService.CreateEditCustomersDialog(this, NavigationService);
    }

    [Command]
    private async Task RemoveCustomer(object param)
    {
        if (param is ObservableCustomer observableCustomer)
        {
            await Task.Run(() =>
            {
                var dialogResult = DialogService.Create(
                    "Customers",
                    $"Are you sure you want to delete {observableCustomer.FullName}?",
                    NavigationService
                );
                if (dialogResult is true)
                {
                    if (observableCustomer == SelectedCustomer)
                    {
                        SelectedCustomer = null!;
                    }
                    GroomWiseDbContext.Customers.Delete(observableCustomer.Id);
                    EventAggregator.Publish(new DeleteCustomerEvent());
                    PopulateCollections();
                }
            });
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
