// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Interfaces.ViewModel;

public interface IAppointmentsViewModel
{
    /// <inheritdoc cref="AppointmentsViewModel._appointments"/>
    SynchronizedObservableCollection<Appointment> Appointments { get; set; }

    /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IRelayCommand"/> instance wrapping <see cref="AppointmentsViewModel.AddAppointment"/>.</summary>
    IRelayCommand AddAppointmentCommand { get; }

    /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IRelayCommand"/> instance wrapping <see cref="AppointmentsViewModel.GetAppointments"/>.</summary>
    IRelayCommand GetAppointmentsCommand { get; }
}
