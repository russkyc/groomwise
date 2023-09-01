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
[Inject(typeof(IDialogService))]
[Inject(typeof(IEventAggregator))]
[Inject(typeof(INavigationService))]
[Inject(typeof(GroomWiseDbContext))]
[RegisterSingleton]
public partial class AppointmentViewModel
    : IEventSubscriber<CreateGroomingServiceEvent>,
        IEventSubscriber<DeleteGroomingServiceEvent>,
        IEventSubscriber<ScheduleAppointmentEvent>,
        IEventSubscriber<DeleteCustomerEvent>
{
    [Property]
    private ObservableAppointment _activeAppointment;

    [Property]
    private ConcurrentObservableCollection<ObservableAppointment> _appointments;

    [Property]
    private ConcurrentObservableCollection<ObservableGroomingService> _groomingServices;

    partial void OnInitialize()
    {
        PopulateCollections();
        ActiveAppointment = new ObservableAppointment { Date = DateTime.Today };
    }

    private async void PopulateCollections()
    {
        await Task.Run(() =>
        {
            var appointments = GroomWiseDbContext.Appointments
                .GetMultiple(appointment => appointment.Date >= DateTime.Today)
                .Select(AppointmentMapper.ToObservable)
                .OrderBy(appointment => appointment.Date);

            var services = GroomWiseDbContext.GroomingServices
                .GetAll()
                .Select(GroomingServiceMapper.ToObservable);

            Appointments = new ConcurrentObservableCollection<ObservableAppointment>(appointments);
            GroomingServices = new ConcurrentObservableCollection<ObservableGroomingService>(
                services
            );
        });
    }

    [Command]
    private async Task CreateAppointment()
    {
        await Task.Run(() =>
        {
            DialogService.CreateAddAppointmentsDialog(this, NavigationService);
        });
    }

    [Command]
    private async Task SaveAppointment()
    {
        await Task.Run(() =>
        {
            var dialogResult = DialogService.Create(
                "GroomWise",
                "Create Appointment?",
                NavigationService
            );
            if (dialogResult is true)
            {
                var appointment = ActiveAppointment.ToEntity();
                GroomWiseDbContext.Appointments.Insert(appointment);
                DialogService.CloseDialogs(NavigationService);
                EventAggregator.Publish(
                    new PublishNotificationEvent($"Appointment saved", NotificationType.Success)
                );
                ActiveAppointment = new ObservableAppointment { Date = DateTime.Today };
                OnPropertyChanged(nameof(ActiveAppointment.Date));
                PopulateCollections();
            }
        });
    }

    [Command]
    private async Task AddGroomingService()
    {
        await Task.Run(
            () => ActiveAppointment.Services.Insert(0, new ObservableAppointmentService())
        );
    }

    [Command]
    private async Task RemoveGroomingService(object param)
    {
        await Task.Run(() =>
        {
            if (param is ObservableAppointmentService service)
            {
                ActiveAppointment.Services.Remove(service);
            }
        });
    }

    public void OnEvent(CreateGroomingServiceEvent eventData)
    {
        PopulateCollections();
    }

    public void OnEvent(DeleteGroomingServiceEvent eventData)
    {
        PopulateCollections();
    }

    public void OnEvent(ScheduleAppointmentEvent eventData)
    {
        ActiveAppointment.Customer = eventData.Customer;
        CreateAppointment();
    }

    public void OnEvent(DeleteCustomerEvent eventData)
    {
        GroomWiseDbContext.Appointments.DeleteMultiple(
            apppintment => apppintment.Customer.Id == eventData.Customer.Id
        );
        PopulateCollections();
    }
}
