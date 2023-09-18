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
[Inject(typeof(GroomWiseDbContext))]
[Inject(typeof(IDialogService))]
[Inject(typeof(INavigationService))]
[Inject(typeof(IEventAggregator))]
public partial class GroomingServiceViewModel
{
    [Property]
    private ObservableGroomingService _activeGroomingService;

    [Property]
    private ConcurrentObservableCollection<ObservableGroomingService> _groomingServices = new();

    partial void OnInitialize()
    {
        PopulateCollections();
    }

    private void PopulateCollections()
    {
        GroomingServices = GroomWiseDbContext.GroomingServices
            .GetAll()
            .Select(GroomingServiceMapper.ToObservable)
            .AsObservableCollection();
    }

    [Command]
    private async Task CreateService()
    {
        ActiveGroomingService = new();
        await DialogService.CreateAddServicesDialog(this, NavigationService);
    }

    [Command]
    private async Task SaveService()
    {
        if (string.IsNullOrEmpty(ActiveGroomingService.Type))
        {
            EventAggregator.Publish(
                new PublishNotificationEvent(
                    "Save Failed",
                    "Service name cannot be blank",
                    NotificationType.Danger
                )
            );
            return;
        }

        var dialogResult = await DialogService.CreateYesNo(
            "GroomWise",
            "Create Service?",
            NavigationService
        );
        if (dialogResult is false)
        {
            return;
        }
        GroomWiseDbContext.GroomingServices.Insert(ActiveGroomingService.ToEntity());
        await DialogService.CloseDialogs(NavigationService);
        EventAggregator.Publish(
            new PublishNotificationEvent(
                "Save Successful",
                $"Service {ActiveGroomingService.Type} saved",
                NotificationType.Success
            )
        );
        EventAggregator.Publish(new CreateGroomingServiceEvent());
        ActiveGroomingService = new();
        PopulateCollections();
    }

    [Command]
    private async Task RemoveService(object param)
    {
        if (param is not ObservableGroomingService observableGroomingService)
        {
            return;
        }
        var dialogResult = await Task.Run(
            () =>
                DialogService.CreateYesNo(
                    "Services",
                    $"Are you sure you want to delete {observableGroomingService.Type}?",
                    NavigationService
                )
        );
        if (dialogResult is false)
        {
            return;
        }
        GroomWiseDbContext.GroomingServices.Delete(observableGroomingService.Id);
        EventAggregator.Publish(new DeleteGroomingServiceEvent());
        EventAggregator.Publish(
            new PublishNotificationEvent(
                "Service List Updated",
                $"{observableGroomingService.Type}' is removed",
                NotificationType.Notify
            )
        );
        PopulateCollections();
    }
}
