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
using GroomWise.Infrastructure.Navigation.Interfaces;
using Injectio.Attributes;
using MvvmGen;
using MvvmGen.Events;
using Swordfish.NET.Collections;

namespace GroomWise.Application.ViewModels;

[ViewModel]
[RegisterSingleton]
[Inject(typeof(IDialogService))]
[Inject(typeof(INavigationService))]
[Inject(typeof(IEventAggregator))]
[Inject(typeof(GroomWiseDbContext))]
public partial class EmployeeViewModel
{
    [Property]
    private ConcurrentObservableCollection<ObservableEmployee> _employees = new();

    [Property]
    private ObservableEmployee _activeEmployee = new();

    [Property]
    private ObservableEmployee _selectedEmployee;

    partial void OnInitialize()
    {
        PopulateCollections();
    }

    private void PopulateCollections()
    {
        Employees = GroomWiseDbContext.Employees
            .GetAll()
            .Select(EmployeeMapper.ToObservable)
            .OrderBy(employee => employee.FullName)
            .AsObservableCollection();
    }

    [Command]
    private async Task CreateEmployee()
    {
        ActiveEmployee = new();
        await DialogService.CreateAddEmployeeDialog(this, NavigationService);
    }

    [Command]
    private void SelectEmployee(object param)
    {
        if (param is not ObservableEmployee employee)
        {
            return;
        }
        SelectedEmployee = employee;
    }

    [Command]
    private async Task EditEmployee()
    {
        await DialogService.CreateEditEmployeeDialog(this, NavigationService);
    }

    [Command]
    private async Task UpdateEmployee()
    {
        var dialogResult = await Task.Run(
            () =>
                DialogService.CreateYesNo(
                    "Employees",
                    $"Update {SelectedEmployee.FullName!.GetFirstName()}?",
                    NavigationService
                )
        );
        if (dialogResult is false)
        {
            return;
        }
        GroomWiseDbContext.Employees.Update(SelectedEmployee.Id, SelectedEmployee.ToEntity());
        EventAggregator.Publish(new UpdateCustomerEvent());
        await DialogService.CloseDialogs(NavigationService);
    }

    [Command]
    private async Task SaveEmployee()
    {
        var dialogResult = await Task.Run(
            () => DialogService.CreateYesNo("Employees", "Save Employee?", NavigationService)
        );

        if (dialogResult is false)
        {
            return;
        }

        if (string.IsNullOrEmpty(ActiveEmployee.FullName))
        {
            EventAggregator.Publish(
                new PublishNotificationEvent(
                    "Saving Failed",
                    "Employee name cannot be empty.",
                    NotificationType.Danger
                )
            );
            return;
        }
        GroomWiseDbContext.Employees.Insert(ActiveEmployee.ToEntity());
        await DialogService.CloseDialogs(NavigationService);
        EventAggregator.Publish(new CreateCustomerEvent());
        EventAggregator.Publish(
            new PublishNotificationEvent(
                "Employees Updated",
                $"Employee {ActiveEmployee.FullName} added.",
                NotificationType.Success
            )
        );
        ActiveEmployee = new();
        PopulateCollections();
    }

    [Command]
    private async Task RemoveEmployee(object param)
    {
        if (param is not ObservableEmployee observableEmployee)
        {
            return;
        }
        var dialogResult = await Task.Run(
            () =>
                DialogService.CreateYesNo(
                    "Employees",
                    $"Are you sure you want to delete {observableEmployee.FullName!.GetFirstName()}?",
                    NavigationService
                )
        );

        if (dialogResult is false)
        {
            return;
        }

        if (observableEmployee == SelectedEmployee)
        {
            SelectedEmployee = null!;
        }

        GroomWiseDbContext.Employees.Delete(observableEmployee.Id);
        EventAggregator.Publish(
            new DeleteEmployeeEvent(observableEmployee.FullName!.GetFirstName())
        );
        EventAggregator.Publish(
            new PublishNotificationEvent(
                "Employees Updated",
                $"{observableEmployee.FullName!.GetFirstName()}' is removed",
                NotificationType.Notify
            )
        );
        PopulateCollections();
    }
}
