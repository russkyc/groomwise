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
using GroomWise.Application.Mappers;
using GroomWise.Application.Observables;
using GroomWise.Infrastructure.Authentication.Interfaces;
using GroomWise.Infrastructure.Database;
using Hangfire;
using Injectio.Attributes;
using MvvmGen;
using MvvmGen.Events;
using Swordfish.NET.Collections;

namespace GroomWise.Application.ViewModels;

[ViewModel]
[RegisterSingleton]
[Inject(typeof(IAuthenticationService))]
[Inject(typeof(GroomWiseDbContext))]
public partial class DashboardViewModel
    : IEventSubscriber<LoginEvent>,
        IEventSubscriber<CreateAppointmentEvent>,
        IEventSubscriber<DeleteAppointmentEvent>
{
    [Property]
    private ConcurrentObservableCollection<ObservableAppointment> _appointments = new();

    [Property]
    private ConcurrentObservableCollection<ObservableAppointment> _upcomingAppointments = new();

    [Property]
    private string _user;

    [Property]
    private DateTime _date;

    partial void OnInitialize()
    {
        PopulateCollections();
        RecurringJob.AddOrUpdate("updateTime", () => SetDate(), Cron.Hourly);
    }

    public async Task SetDate()
    {
        Date = DateTime.Now;
        await Task.Delay(10);
    }

    private void PopulateCollections()
    {
        Task.Run(async () =>
        {
            var appointments = await Task.Run(
                () =>
                    GroomWiseDbContext.Appointments
                        .GetMultiple(appointment => appointment.Date == DateTime.Today)
                        .Select(AppointmentMapper.ToObservable)
                        .OrderBy(appointment => appointment.Date)
            );

            var upcomingAppointments = await Task.Run(
                () =>
                    GroomWiseDbContext.Appointments
                        .GetMultiple(
                            appointment =>
                                appointment.Date > DateTime.Today
                                && appointment.Date < DateTime.Today.AddDays(7)
                        )
                        .Select(AppointmentMapper.ToObservable)
                        .OrderBy(appointment => appointment.Date)
                        .ThenBy(appointment => appointment.StartTime)
            );

            Appointments = new ConcurrentObservableCollection<ObservableAppointment>(appointments);
            UpcomingAppointments = new ConcurrentObservableCollection<ObservableAppointment>(
                upcomingAppointments
            );
        });
    }

    public void OnEvent(LoginEvent eventData)
    {
        User = AuthenticationService.GetSession()?.FirstName!;
    }

    public void OnEvent(CreateAppointmentEvent eventData)
    {
        PopulateCollections();
    }

    public void OnEvent(DeleteAppointmentEvent eventData)
    {
        PopulateCollections();
    }
}
