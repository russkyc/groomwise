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
using GroomWise.Domain.Enums;
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
public partial class CustomerViewModel
{
    [Property]
    private ObservableCustomer _activeCustomer = new ObservableCustomer();

    [Property]
    private ObservableCustomer _selectedCustomer;

    [Property]
    private ConcurrentObservableCollection<ObservableCustomer> _customers = new();

    partial void OnInitialize()
    {
        PopulateCollections();
    }

    private void PopulateCollections()
    {
        Customers = GroomWiseDbContext.Customers
            .GetAll()
            .Select(CustomerMapper.ToObservable)
            .OrderBy(customer => customer.FullName)
            .AsObservableCollection();
    }

    [Command]
    private async Task CreateCustomer()
    {
        ActiveCustomer = new();
        await DialogService.CreateAddCustomersDialog(this, NavigationService);
    }

    [Command]
    private async Task SaveCustomer()
    {
        var dialogResult = await Task.Run(
            () => DialogService.CreateYesNo("GroomWise", "Save Customer?", NavigationService)
        );
        if (dialogResult is false)
        {
            return;
        }
        if (string.IsNullOrEmpty(ActiveCustomer.FullName))
        {
            EventAggregator.Publish(
                new PublishNotificationEvent(
                    "Save Failed",
                    "Customer name cannot be empty.",
                    NotificationType.Danger
                )
            );
            return;
        }
        GroomWiseDbContext.Customers.Insert(ActiveCustomer.ToEntity());
        await DialogService.CloseDialogs(NavigationService);
        EventAggregator.Publish(new CreateCustomerEvent());
        EventAggregator.Publish(
            new PublishNotificationEvent(
                "Customer Added",
                $"{ActiveCustomer.FullName} is saved to Customers",
                NotificationType.Success
            )
        );
        ActiveCustomer = new ObservableCustomer();
        PopulateCollections();
    }

    [Command]
    private void SelectCustomer(object param)
    {
        if (param is not ObservableCustomer customer)
        {
            return;
        }
        SelectedCustomer = customer;
    }

    [Command]
    private void AddCustomerPet()
    {
        ActiveCustomer.Pets.Insert(0, new ObservablePet());
    }

    [Command]
    private void AddSelectedCustomerPet()
    {
        SelectedCustomer.Pets.Insert(0, new ObservablePet());
    }

    [Command]
    private void RemoveCustomerPet(object param)
    {
        if (param is not ObservablePet pet)
        {
            return;
        }
        ActiveCustomer.Pets.Remove(pet);
    }

    [Command]
    private async Task RemoveSelectedCustomerPet(object param)
    {
        if (param is not ObservablePet pet)
        {
            return;
        }
        var dialogResult = await Task.Run(
            () =>
                DialogService.CreateYesNo(
                    $"{SelectedCustomer.FullName.GetFirstName()}'s Pets",
                    $"Are you sure you want to remove {pet.Name}?",
                    NavigationService
                )
        );
        if (dialogResult is false)
        {
            return;
        }
        SelectedCustomer.Pets.Remove(pet);
        GroomWiseDbContext.Customers.Update(SelectedCustomer.Id, SelectedCustomer.ToEntity());
        EventAggregator.Publish(new UpdateCustomerEvent());
    }

    [Command]
    private async Task UpdateCustomer()
    {
        var dialogResult = await Task.Run(
            () =>
                DialogService.CreateYesNo(
                    "Appointments",
                    $"Update {SelectedCustomer.FullName.GetFirstName()}?",
                    NavigationService
                )
        );
        if (dialogResult is false)
        {
            return;
        }
        GroomWiseDbContext.Customers.Update(SelectedCustomer.Id, SelectedCustomer.ToEntity());
        EventAggregator.Publish(new UpdateCustomerEvent());
        await DialogService.CloseDialogs(NavigationService);
    }

    [Command]
    private async Task EditCustomer()
    {
        await DialogService.CreateEditCustomersDialog(this, NavigationService);
    }

    [Command]
    private async Task RemoveCustomer(object param)
    {
        if (param is not ObservableCustomer observableCustomer)
        {
            return;
        }
        var dialogResult = await Task.Run(
            () =>
                DialogService.CreateYesNo(
                    "Customers",
                    $"Are you sure you want to delete {observableCustomer.FullName.GetFirstName()}?",
                    NavigationService
                )
        );

        if (dialogResult is false)
        {
            return;
        }

        if (observableCustomer == SelectedCustomer)
        {
            SelectedCustomer = null!;
        }
        GroomWiseDbContext.Customers.Delete(observableCustomer.Id);
        EventAggregator.Publish(new DeleteCustomerEvent(observableCustomer));
        EventAggregator.Publish(
            new PublishNotificationEvent(
                "Customer List Updated",
                $"{observableCustomer.FullName.GetFirstName()}' is removed",
                NotificationType.Notify
            )
        );
        PopulateCollections();
    }

    [Command]
    private void ScheduleAppointment(object param)
    {
        if (param is not ObservableCustomer customer)
        {
            return;
        }
        var appointmentsViewModel = AppServicesContainer.GetService<AppointmentViewModel>();
        if (appointmentsViewModel is not null)
        {
            EventAggregator.Publish(new ScheduleAppointmentEvent(customer));
        }
    }

    [Command]
    private void CloseDialogs()
    {
        DialogService.CloseDialogs(NavigationService);
    }
}
