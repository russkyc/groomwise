// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class AppointmentsViewModel : ViewModelBase, IAppointmentsViewModel
{
    private IAddAppointmentsViewFactory _addAppointmentsViewFactory;
    private IAppointmentsRepository _appointmentsRepository;

    [ObservableProperty]
    private AppointmentsCollection _appointments;

    public AppointmentsViewModel(
        IAppointmentsRepository appointmentsRepository,
        IAddAppointmentsViewFactory addAppointmentsViewFactory
    )
    {
        _appointmentsRepository = appointmentsRepository;
        _addAppointmentsViewFactory = addAppointmentsViewFactory;

        Appointments = new AppointmentsCollection();

        GetAppointments();
    }

    void GetAppointments()
    {
        Task.Run(async () =>
        {
            await DispatchHelper.UiInvokeAsync(
                () => Appointments.AddRange(_appointmentsRepository.GetCollection())
            );
        });
    }

    [RelayCommand]
    void AddAppointment()
    {
        Task.Run(async () =>
        {
            await DispatchHelper.UiInvokeAsync(
                () =>
                    _addAppointmentsViewFactory
                        .Create(addAppointmentsView => addAppointmentsView.AsChild())
                        .Show()
            );
        });
    }
}
