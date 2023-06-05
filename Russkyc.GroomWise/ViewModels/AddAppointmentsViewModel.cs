// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class AddAppointmentsViewModel : ViewModelBase, IAddAppointmentsViewModel
{
    private readonly IAppointmentsRepository _appointmentsRepository;

    [ObservableProperty]
    private Appointment _newAppointment;

    public AddAppointmentsViewModel(IAppointmentsRepository appointmentsRepository)
    {
        _appointmentsRepository = appointmentsRepository;
    }

    [RelayCommand]
    void AddAppointment()
    {
        _appointmentsRepository.Add(NewAppointment);
    }
}
