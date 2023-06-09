﻿// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class AppointmentsViewModel : ViewModelBase, IAppointmentsViewModel
{
    private readonly ILogger _logger;
    private readonly ISchedulerService _schedulerService;
    private readonly AddAppointmentsViewFactory _addAppointmentsViewFactory;

    private readonly IUnitOfWork _dbContext;

    [ObservableProperty]
    private SynchronizedObservableCollection<Appointment> _appointments;

    public AppointmentsViewModel(
        ILogger logger,
        IUnitOfWork dbContext,
        ISchedulerService schedulerService,
        AddAppointmentsViewFactory addAppointmentsViewFactory
    )
    {
        _logger = logger;
        _dbContext = dbContext;
        _schedulerService = schedulerService;
        _addAppointmentsViewFactory = addAppointmentsViewFactory;

        Appointments = new SynchronizedObservableCollection<Appointment>();

        GetAppointments();
    }

    [RelayCommand]
    void GetAppointments()
    {
        _schedulerService.RunPeriodically(
            () =>
            {
                var command = new SynchronizeCollectionCommand<
                    Appointment,
                    SynchronizedObservableCollection<Appointment>
                >(
                    ref _appointments,
                    _dbContext.AppointmentRepository
                        .GetAll()
                        .Where(appointment => appointment.Date.Value.Month >= DateTime.Now.Month)
                        .ToList()
                );
                command.Execute();
                _logger.Log(this, "Synchronized appointments collection");
            },
            TimeSpan.FromSeconds(2)
        );
    }

    [RelayCommand]
    void AddAppointment()
    {
        _addAppointmentsViewFactory
            .Create(addAppointmentsView => addAppointmentsView.AsChild())
            .Show();
    }
}
