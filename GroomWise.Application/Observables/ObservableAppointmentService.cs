﻿// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using Lombok.NET;

namespace GroomWise.Application.Observables;

[NotifyPropertyChanged]
public partial class ObservableAppointmentService
{
    [Property(PropertyChangeType = PropertyChangeType.PropertyChanged)]
    private ObservableGroomingService _groomingService;
}