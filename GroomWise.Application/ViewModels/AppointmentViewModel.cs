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
using GroomWise.Domain.Filters;
using GroomWise.Infrastructure.Database;
using GroomWise.Infrastructure.Logging.Interfaces;
using GroomWise.Infrastructure.Navigation.Interfaces;
using GroomWise.Infrastructure.Storage.Interfaces;
using Injectio.Attributes;
using MvvmGen;
using MvvmGen.Events;
using Swordfish.NET.Collections;
using Swordfish.NET.Collections.Auxiliary;

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
        IEventSubscriber<DeleteCustomerEvent>,
        IEventSubscriber<CreateCustomerEvent>,
        IEventSubscriber<UpdateCustomerEvent>
{
    [Property]
    private ObservableAppointment _activeAppointment;

    [Property]
    private ObservableAppointment? _selectedAppointment;

    [Property]
    private ConcurrentObservableCollection<ObservableAppointment> _appointments = new();

    [Property]
    private ConcurrentObservableCollection<ObservableCustomer> _customers = new();

    [Property]
    private ConcurrentObservableCollection<ObservableGroomingService> _groomingServices = new();

    [Property]
    private ConcurrentObservableCollection<TimeOnly> _bookingTimes = new();

    partial void OnInitialize()
    {
        PopulateCollections();
        ActiveAppointment = new ObservableAppointment { Date = DateTime.Today };
    }

    private void PopulateCollections()
    {
        Customers = GroomWiseDbContext.Customers
            .GetAll()
            .Select(CustomerMapper.ToObservable)
            .OrderBy(customer => customer.FullName)
            .AsObservableCollection();
        Appointments = GroomWiseDbContext.Appointments
            .GetMultiple(AppointmentFilters.IsToday)
            .Select(AppointmentMapper.ToObservable)
            .OrderBy(appointment => appointment.Date)
            .ThenBy(appointment => appointment.StartTime)
            .AsObservableCollection();
        GroomingServices = GroomWiseDbContext.GroomingServices
            .GetAll()
            .Select(GroomingServiceMapper.ToObservable)
            .AsObservableCollection();
    }

    [Command]
    private async Task CreateAppointment(object param)
    {
        await GenerateBookingTimes();
        if (param is ObservableCustomer customer)
        {
            ActiveAppointment = new ObservableAppointment
            {
                Date = DateTime.Today,
                Customer = customer
            };

            if (BookingTimes.Count > 0)
            {
                ActiveAppointment.StartTime = BookingTimes.First();
            }
            await DialogService.CreateAddAppointmentsDialog(this, NavigationService);
            return;
        }
        ActiveAppointment = new ObservableAppointment { Date = DateTime.Today };
        if (BookingTimes.Count > 0)
        {
            ActiveAppointment.StartTime = BookingTimes.First();
        }
        await DialogService.CreateCustomerSelectionDialog(this, NavigationService);
        OnPropertyChanged(nameof(ActiveAppointment.Date));
    }

    [Command]
    private async Task CancelAppointment(object param, bool noConfirmation = false)
    {
        if (param is not ObservableAppointment appointment)
        {
            return;
        }
        if (noConfirmation)
        {
            Appointments.Remove(appointment);
            GroomWiseDbContext.Appointments.Delete(appointment.Id);
            EventAggregator.Publish(new DeleteAppointmentEvent());
            EventAggregator.Publish(
                new PublishNotificationEvent(
                    "Appointment Cancelled",
                    $"{appointment.Customer.FullName.GetFirstName()}'s appointment is cancelled.",
                    NotificationType.Notify
                )
            );
            return;
        }
        var dialogResult = await DialogService.CreateYesNo(
            "Appointments",
            $"Cancel {appointment.Customer.FullName.Split(" ")[0]}'s Appointment?",
            NavigationService
        );

        if (dialogResult is false)
        {
            return;
        }

        Appointments.Remove(appointment);
        GroomWiseDbContext.Appointments.Delete(appointment.Id);

        EventAggregator.Publish(new DeleteAppointmentEvent());
        EventAggregator.Publish(
            new PublishNotificationEvent(
                "Appointment Cancelled",
                $"{appointment.Customer.FullName.GetFirstName()}'s appointment is cancelled.",
                NotificationType.Notify
            )
        );

        if (SelectedAppointment is null)
        {
            return;
        }
        if (appointment.Id == SelectedAppointment.Id)
        {
            SelectedAppointment = null;
        }
        await GenerateBookingTimes();
    }

    [Command]
    private async Task SelectCustomer(object param)
    {
        if (param is not ObservableCustomer customer)
        {
            return;
        }
        ActiveAppointment.Customer = customer;
        await DialogService.CloseDialogs(NavigationService);
        await Task.Delay(100);
        await DialogService.CreateAddAppointmentsDialog(this, NavigationService);
    }

    [Command]
    private void SelectAppointment(object param)
    {
        if (param is not ObservableAppointment appointment)
        {
            return;
        }

        SelectedAppointment = appointment;
    }

    [Command]
    private async Task SaveAppointment()
    {
        if (ActiveAppointment.Services.IsNullOrEmpty())
        {
            EventAggregator.Publish(
                new PublishNotificationEvent(
                    "Save Failed",
                    "Cannot create an appointment if there are no services.",
                    NotificationType.Danger
                )
            );
            return;
        }
        var dialogResult = await Task.Run(
            () =>
                DialogService.CreateYesNo(
                    "Appointments",
                    $"Book Appointment for {ActiveAppointment.Customer.FullName.Split(" ")[0]}?",
                    NavigationService
                )
        );
        if (dialogResult is false)
        {
            return;
        }
        await DialogService.CloseDialogs(NavigationService);
        GroomWiseDbContext.Appointments.Insert(ActiveAppointment.ToEntity());
        EventAggregator.Publish(new CreateAppointmentEvent());
        EventAggregator.Publish(
            new PublishNotificationEvent(
                "Appointment Booked",
                $"{ActiveAppointment.Customer.FullName.GetFirstName()}'s appointment is scheduled on {ActiveAppointment.Date:MMM-dd ddd}",
                NotificationType.Success
            )
        );

        ActiveAppointment = new ObservableAppointment { Date = DateTime.Today };
        OnPropertyChanged(nameof(ActiveAppointment.Date));
        PopulateCollections();
    }

    [Command]
    private async Task AddGroomingService()
    {
        if (GroomingServices.Count == 0)
        {
            await Task.Run(() =>
            {
                DialogService.CreateOk(
                    "Appointments",
                    "There are currently no available services.",
                    NavigationService
                );
            });
            return;
        }
        ActiveAppointment.Services!.Insert(
            0,
            new ObservableAppointmentService { GroomingService = GroomingServices.First() }
        );
        await CalculateAppointmentTimeSpan();
    }

    [Command]
    private async Task RemoveGroomingService(object param)
    {
        if (param is not ObservableAppointmentService service)
        {
            return;
        }
        if (ActiveAppointment.Services.IsNullOrEmpty())
        {
            return;
        }
        if (ActiveAppointment.Services!.Contains(service))
        {
            ActiveAppointment.Services!.Remove(service);
            await CalculateAppointmentTimeSpan();
        }
    }

    [Command]
    private async Task CalculateAppointmentTimeSpan()
    {
        ActiveAppointment.EndTime = await Task.Run(() =>
        {
            TimeSpan timeSpan = new TimeSpan();
            ActiveAppointment.Services.ForEach(groomingService =>
            {
                if (groomingService.GroomingService is not null)
                {
                    timeSpan = timeSpan.Add(groomingService.GroomingService.Duration);
                }
            });
            return ActiveAppointment.StartTime.Add(timeSpan);
        });

        if (
            Appointments
                .Where(appointment => appointment.Date.Date == ActiveAppointment.Date.Date)
                .Any(
                    appointment =>
                        ActiveAppointment
                            .ToEntity()
                            .IsOverlapping(appointment.StartTime, appointment.EndTime)
                )
        )
        {
            if (ActiveAppointment.Services.IsNullOrEmpty())
            {
                return;
            }
            var dialogResult = await Task.Run(
                () =>
                    DialogService.CreateOk(
                        "Appointments",
                        $"Appointment Service is overlapping other appointments. Remove Service {ActiveAppointment.Services!.FirstOrDefault()!.GroomingService!.Type}?",
                        NavigationService
                    )
            );
            if (dialogResult is true)
            {
                await RemoveGroomingService(ActiveAppointment.Services!.First());
            }
        }
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
        Task.Run(async () => await CreateAppointment(eventData.Customer));
    }

    public void OnEvent(DeleteCustomerEvent eventData)
    {
        GroomWiseDbContext.Appointments.DeleteMultiple(
            apppintment => apppintment.Customer.Id == eventData.Customer.Id
        );
        PopulateCollections();
    }

    [Command]
    async Task GenerateBookingTimes()
    {
        BookingTimes = await Task.Run(() =>
        {
            var range = Enumerable
                .Range(0, (int)(new TimeOnly(16, 30) - new TimeOnly(7, 0)).TotalMinutes / 30 + 1)
                .Select(i => new TimeOnly(7, 0).Add(TimeSpan.FromMinutes(30 * i)));

            if (
                Appointments.Any(
                    appointment => appointment.Date.Date == ActiveAppointment.Date.Date
                )
            )
            {
                return new ConcurrentObservableCollection<TimeOnly>(
                    range.Where(
                        time =>
                            Appointments
                                .Where(
                                    appointment =>
                                        appointment.Date.Date == ActiveAppointment.Date.Date
                                )
                                .All(
                                    appointment =>
                                        !time.IsBetween(appointment.StartTime, appointment.EndTime)
                                )
                    )
                );
            }

            return new ConcurrentObservableCollection<TimeOnly>(range);
        });
    }

    public void OnEvent(CreateCustomerEvent eventData)
    {
        PopulateCollections();
    }

    public void OnEvent(UpdateCustomerEvent eventData)
    {
        PopulateCollections();
    }
}
