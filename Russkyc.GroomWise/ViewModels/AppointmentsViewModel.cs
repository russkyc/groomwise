// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class AppointmentsViewModel : ObservableObject, IAppointmentsViewModel
{
    private IAppointmentScheduleFactory _appointmentScheduleFactoryService;
    private IAppointmentsRepositoryService _appointmentsRepositoryService;
    private IAppointmentFactoryService _appointmentFactory;

    [ObservableProperty]
    private AppointmentsScheduleCollection _appointments;
    public AppointmentsViewModel(
        IAppointmentFactoryService appointmentFactory,
        IAppointmentScheduleFactory appointmentScheduleFactory,
        IAppointmentsRepositoryService appointmentsRepositoryService)
    {
        _appointmentScheduleFactoryService = appointmentScheduleFactory;
        _appointmentsRepositoryService = appointmentsRepositoryService;
        _appointmentFactory = appointmentFactory;
        
        Appointments = new AppointmentsScheduleCollection();
        
        GetAppointments();
    }

    void GetAppointments()
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
}