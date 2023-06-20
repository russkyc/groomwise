// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels.Appointments;

public partial class AppointmentsViewModel : ViewModelBase, IAppointmentsViewModel
{
    private readonly ILogger _logger;
    private readonly ISchedulerService _schedulerService;
    private readonly IFactory<AddAppointmentsView> _addAppointmentsViewFactory;

    private readonly IDbContext _dbContext;

    [ObservableProperty]
    private SynchronizedObservableCollection<Appointment> _appointments;

    public AppointmentsViewModel(
        ILogger logger,
        IDbContext dbContext,
        ISchedulerService schedulerService,
        IFactory<AddAppointmentsView> addAppointmentsViewFactory
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
        var command = new SynchronizeCollectionCommand<
            Appointment,
            SynchronizedObservableCollection<Appointment>
        >(
            ref _appointments,
            _dbContext.AppointmentRepository
                .GetAll()
                .Where(appointment => appointment.Date.Value.Month >= DateTime.Now.Month)
                .OrderBy(appointment => appointment.Date)
                .ToList()
        );
        command.Execute();
        _logger.Log(this, "Synchronized appointments collection");
    }

    [RelayCommand]
    void AddAppointment()
    {
        _addAppointmentsViewFactory
            .Create()
            .Show();
    }
}
