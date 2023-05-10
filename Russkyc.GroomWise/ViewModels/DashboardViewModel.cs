// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class DashboardViewModel : ViewModelBase, IDashboardViewModel
{
    private readonly IAppointmentFactory _appointmentFactory;
    private readonly IEncryptionService _encryptionService;

    [ObservableProperty]
    private ISessionManagerService _sessionManagerService;

    [ObservableProperty]
    private AppointmentsCollection _appointments;

    [ObservableProperty]
    private string? _user;

    [ObservableProperty]
    private DateTime _date;

    public DashboardViewModel(
        IAppointmentFactory appointmentFactory,
        ISessionManagerService sessionManagerService,
        IEncryptionService encryptionService
    )
    {
        _appointmentFactory = appointmentFactory;
        SessionManagerService = sessionManagerService;
        _encryptionService = encryptionService;

        Appointments = new AppointmentsCollection();
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
        Task.Run(async () =>
        {
            lock (Appointments.Lock)
                Appointments.Clear();
            for (var i = 0; i < 50; i++)
            {
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    lock (Appointments.Lock)
                    {
                        Appointments.Insert(
                            0,
                            _appointmentFactory.Create(
                                $"Appointment {i}",
                                $"Service scheduled for today",
                                DateTime.Now
                            )
                        );
                    }
                });
                await Task.Delay(2500);
            }
        });
    }

    private void GetWelcomeMessage()
    {
        User = _encryptionService.Decrypt(SessionManagerService.SessionUser!.FirstName!);
    }

    private void GetTime()
    {
        Date = DateTime.Now;
    }
}
