// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class AppointmentsViewModel : ViewModelBase, IAppointmentsViewModel
{
    private readonly ILogger _logger;
    private readonly ISchedulerService _schedulerService;
    private readonly AddAppointmentsViewFactory _addAppointmentsViewFactory;

    private readonly AppointmentRepository _appointmentRepository;

    [ObservableProperty]
    private SynchronizedObservableCollection<Appointment> _appointments;

    public AppointmentsViewModel(
        ILogger logger,
        ISchedulerService schedulerService,
        AppointmentRepository appointmentRepository,
        AddAppointmentsViewFactory addAppointmentsViewFactory
    )
    {
        _logger = logger;
        _schedulerService = schedulerService;
        _appointmentRepository = appointmentRepository;
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
                    _appointmentRepository
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
        Task.Run(async () =>
        {
            await DispatchHelper.UiInvokeAsync(
                () =>
                    _addAppointmentsViewFactory
                        .Create(addAppointmentsView => addAppointmentsView.AsChild())
                        .Show()
            );
        });
    }
}
