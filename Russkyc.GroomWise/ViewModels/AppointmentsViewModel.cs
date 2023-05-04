// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public class AppointmentsViewModel : ObservableObject, IAppointmentsViewModel
{
    private IAppointmentFactoryService _appointmentFactory;
    private IAppointmentsRepositoryService _appointmentsRepositoryService;

    public AppointmentsViewModel(
        IAppointmentFactoryService appointmentFactory,
        IAppointmentsRepositoryService appointmentsRepositoryService)
    {
        _appointmentFactory = appointmentFactory;
        _appointmentsRepositoryService = appointmentsRepositoryService;
    }
}