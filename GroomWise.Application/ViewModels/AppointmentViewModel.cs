// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Collections.ObjectModel;
using GroomWise.Application.Mappers;
using GroomWise.Application.Observables;
using GroomWise.Infrastructure.Database;
using GroomWise.Infrastructure.Logging.Interfaces;
using GroomWise.Infrastructure.Storage.Interfaces;
using MvvmGen;

namespace GroomWise.Application.ViewModels;

[ViewModel]
[ViewModelGenerateInterface]
[Inject(typeof(ILogger))]
[Inject(typeof(IFileStorage))]
[Inject(typeof(GroomWiseDbContext))]
public partial class AppointmentViewModel
{
    [Property]
    private ObservableAppointment _activeAppointment;

    [Property]
    private ObservableCollection<ObservableAppointment> _appointments;

    [Property]
    private ObservableCollection<ObservableGroomingService> _groomingServices;

    partial void OnInitialize()
    {
        PopulateCollections();
    }

    private void PopulateCollections()
    {
        var appointments = GroomWiseDbContext!.Appointments
            .GetAll()
            .Select(AppointmentMapper.ToObservable);

        var groomingServices = GroomWiseDbContext!.GroomingServices
            .GetAll()
            .Select(GroomingServiceMapper.ToObservable);

        Appointments = new ObservableCollection<ObservableAppointment>(appointments);
        GroomingServices = new ObservableCollection<ObservableGroomingService>(groomingServices);
    }
}
