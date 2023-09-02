// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using MvvmGen;

namespace GroomWise.Application.Observables;

[ViewModel]
public partial class ObservableAppointmentService
{
    [Property]
    [PropertyCallMethod(nameof(ServiceChanged))]
    private ObservableGroomingService? _groomingService;

    private void ServiceChanged()
    {
        OnPropertyChanged(nameof(GroomingService.TimeSpan));
    }
}
