// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class DashboardViewModel : ViewModelBase, IDashboardViewModel
{
    private readonly IAppointmentScheduleFactory _appointmentScheduleFactoryService;
    private readonly IApplicationService _applicationService;
    
    [ObservableProperty]
    private ISessionManagerService _sessionManagerService;
    
    [ObservableProperty]
    private AppointmentsScheduleCollection _appointments;
    

    [ObservableProperty]
    private string? _welcomeMessage;

    public DashboardViewModel(
        ISessionManagerService sessionManagerService,
        IAppointmentScheduleFactory appointmentScheduleFactoryService,
        IApplicationService applicationService)
    {
        _appointmentScheduleFactoryService = appointmentScheduleFactoryService;
        SessionManagerService = sessionManagerService;
        _applicationService = applicationService;
        
        Appointments = new AppointmentsScheduleCollection();
        
        Invalidate();
    }

    public void Invalidate()
    {
        GetWelcomeMessage();
        GetNotifications();
    }

    [RelayCommand]
    private void GetNotifications()
    {
        Task.Run(
            () =>
            {
                for (var i = 8; i < 12; i++)
                {
                    Appointments.Insert(
                        0,
                        _appointmentScheduleFactoryService.Create(
                            "AM",
                            $"{i}:00",
                            $"{i}",
                            $"Appointment {i}",
                            $"Service scheduled for today"));
                    Thread.Sleep(1000);
                }
            });
    }

    [RelayCommand]
    private void GetWelcomeMessage()
    {
        WelcomeMessage = $"Good Morning, {SessionManagerService.SessionUser?.FirstName}!";
    }

}