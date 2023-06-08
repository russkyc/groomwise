// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class DashboardViewModel : ViewModelBase, IDashboardViewModel
{
    private readonly AppointmentRepository _appointmentRepository;
    private readonly AppointmentFactory _appointmentFactory;
    private readonly IEncryptionService _encryptionService;
    private readonly ISchedulerService _schedulerService;

    [ObservableProperty]
    private ISessionManagerService _sessionManagerService;

    [ObservableProperty]
    private SynchronizedObservableCollection<Appointment> _appointments;

    [ObservableProperty]
    private string? _user;

    [ObservableProperty]
    private DateTime _date;

    public DashboardViewModel(
        AppointmentFactory appointmentFactory,
        ISessionManagerService sessionManagerService,
        IEncryptionService encryptionService,
        AppointmentRepository appointmentRepository,
        ISchedulerService schedulerService
    )
    {
        _appointmentFactory = appointmentFactory;
        SessionManagerService = sessionManagerService;
        _encryptionService = encryptionService;
        _appointmentRepository = appointmentRepository;
        _schedulerService = schedulerService;

        Appointments = new SynchronizedObservableCollection<Appointment>();
        GetNotifications();
    }

    [RelayCommand]
    public void Invalidate()
    {
        GetWelcomeMessage();
        GetTime();
    }

    private void GetNotifications()
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
                        .FindAll(
                            appointment =>
                                appointment.Date!.Value.Day == DateTime.Today.Day
                                && appointment.Date!.Value.Month == DateTime.Today.Month
                                && appointment.Date!.Value.Hour >= DateTime.Today.Hour
                        )
                        .OrderBy(appointment => appointment.Date!.Value.TimeOfDay)
                        .ToList()
                );
                command.Execute();
            },
            TimeSpan.FromSeconds(2)
        );
    }

    private void GetWelcomeMessage()
    {
        User = SessionManagerService.SessionUser!.FirstName;
    }

    private void GetTime()
    {
        Date = DateTime.Now;
    }
}
