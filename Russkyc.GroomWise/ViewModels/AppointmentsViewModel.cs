// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class AppointmentsViewModel : ViewModelBase, IAppointmentsViewModel
{
    private IAppointmentFactory _appointmentFactory;
    private IAddAppointmentsViewFactory _addAppointmentsViewFactory;
    private IAppointmentsRepository _appointmentsRepository;

    [ObservableProperty]
    private AppointmentsCollection _appointments;

    public AppointmentsViewModel(
        IAppointmentFactory appointmentFactory,
        IAppointmentsRepository appointmentsRepository,
        IAddAppointmentsViewFactory addAppointmentsViewFactory
    )
    {
        _appointmentFactory = appointmentFactory;
        _appointmentsRepository = appointmentsRepository;
        _addAppointmentsViewFactory = addAppointmentsViewFactory;

        Appointments = new AppointmentsCollection();

        GetAppointments();
    }

    void GetAppointments()
    {
        Task.Run(async () =>
        {
            lock (Appointments.Lock)
                Appointments.Clear();
            for (var i = 0; i < 50; i++)
            {
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    lock (Appointments.Lock)
                        Appointments.Add(
                            _appointmentFactory.Create(
                                $"Appointment {i}",
                                $"Service scheduled for {DateTime.Now.AddDays(i).ToString("dddd")}",
                                DateTime.Now.AddDays(i)
                            )
                        );
                });
                await Task.Delay(2500);
            }
        });
    }

    [RelayCommand]
    void AddAppointment()
    {
        Task.Run(async () =>
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                _addAppointmentsViewFactory.Create(BuilderServices.Resolve<IMainView>()).Show();
            });
        });
    }
}
