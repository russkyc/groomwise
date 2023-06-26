// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels.Dashboard;

public partial class DashboardViewModel : ViewModelBase, IDashboardViewModel
{
    private readonly IDbContext _dbContext;
    private readonly ISchedulerService _schedulerService;
    private readonly IEncryptionService _encryptionService;
    private readonly IFactory<Appointment> _appointmentFactory;

    [ObservableProperty]
    private ISessionManagerService _sessionManagerService;

    [ObservableProperty]
    private SynchronizedObservableCollection<Appointment> _appointments;

    [ObservableProperty]
    private string? _user;

    [ObservableProperty]
    private DateTime _date;

    public DashboardViewModel(
        IFactory<Appointment> appointmentFactory,
        ISessionManagerService sessionManagerService,
        IEncryptionService encryptionService,
        ISchedulerService schedulerService,
        IDbContext dbContext
    )
    {
        _appointmentFactory = appointmentFactory;
        SessionManagerService = sessionManagerService;
        _encryptionService = encryptionService;
        _schedulerService = schedulerService;
        _dbContext = dbContext;

        Appointments = new SynchronizedObservableCollection<Appointment>();
        ReloadNotifications();
    }

    [RelayCommand]
    public void Invalidate()
    {
        GetWelcomeMessage();
        GetTime();
    }

    public void ReloadNotifications()
    {
        var command = new SynchronizeCollectionCommand<
            Appointment,
            SynchronizedObservableCollection<Appointment>
        >(
            ref _appointments,
            _dbContext.AppointmentRepository
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
    }

    private void GetWelcomeMessage()
    {
        User = SessionManagerService.Session!.SessionUser?.FirstName;
    }

    private void GetTime()
    {
        Date = DateTime.Now;
    }
}
