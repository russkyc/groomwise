// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Interfaces.ViewModel;

public interface IAppointmentsViewModel
{
    /// <inheritdoc cref="AppointmentsViewModel._appointments"/>
    global::GroomWise.Models.Collections.AppointmentsCollection Appointments
    {
        get;
        [global::System.Diagnostics.CodeAnalysis.MemberNotNull("_appointments")]
        set;
    }

    /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IRelayCommand"/> instance wrapping <see cref="AppointmentsViewModel.AddAppointment"/>.</summary>
    global::CommunityToolkit.Mvvm.Input.IRelayCommand AddAppointmentCommand { get; }

    /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IRelayCommand"/> instance wrapping <see cref="AppointmentsViewModel.GetAppointments"/>.</summary>
    global::CommunityToolkit.Mvvm.Input.IRelayCommand GetAppointmentsCommand { get; }
}