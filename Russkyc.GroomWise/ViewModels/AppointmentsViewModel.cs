// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using GroomWise.Models.Interfaces;

namespace GroomWise.ViewModels;

public class AppointmentsViewModel : ObservableObject, IAppointmentsViewModel
{
    private IAppointmentFactoryService _appointmentFactory;
    private IAppointmentsRepository _appointmentsRepository;

    public AppointmentsViewModel(
        IAppointmentFactoryService appointmentFactory,
        IAppointmentsRepository appointmentsRepository)
    {
        _appointmentFactory = appointmentFactory;
        _appointmentsRepository = appointmentsRepository;
    }
}