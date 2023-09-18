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

using System.Media;
using GroomWise.Application.Events;
using GroomWise.Application.Extensions;
using GroomWise.Application.Mappers;
using GroomWise.Application.Observables;
using GroomWise.Domain.Filters;
using GroomWise.Infrastructure.Authentication.Interfaces;
using GroomWise.Infrastructure.Database;
using GroomWise.Infrastructure.Scheduler.Enums;
using GroomWise.Infrastructure.Scheduler.Interfaces;
using Injectio.Attributes;
using MvvmGen;
using MvvmGen.Events;
using Swordfish.NET.Collections;

namespace GroomWise.Application.ViewModels;

[ViewModel]
[RegisterSingleton]
[Inject(typeof(IScheduler))]
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
        Scheduler.ScheduleTask(NotifyOnAppointmentStart, 1, Span.Minutes);
        Scheduler.ScheduleTask(SetDate, 30, Span.Minutes);
    }

    public void NotifyOnAppointmentStart()
    {
        var appointment = Appointments.FirstOrDefault();

        if (appointment is null)
        {
            return;
        }

        if (appointment.Date.Day != DateTime.Today.Day)
        {
            return;
        }

        if (appointment.StartTime != TimeOnly.FromDateTime(DateTime.Now))
        {
            return;
        }
    }

    public void SetDate()
    {
        Date = DateTime.Now;
    }

    private void PopulateCollections()
    {
        UpcomingAppointments = GroomWiseDbContext.Appointments
            .GetMultiple(AppointmentFilters.IsThisWeek)
            .Select(AppointmentMapper.ToObservable)
            .OrderBy(appointment => appointment.Date)
            .ThenBy(appointment => appointment.StartTime)
            .AsObservableCollection();
        Appointments = GroomWiseDbContext.Appointments
            .GetMultiple(AppointmentFilters.IsToday)
            .OrderBy(appointment => appointment.Date)
            .Select(AppointmentMapper.ToObservable)
            .AsObservableCollection();
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
