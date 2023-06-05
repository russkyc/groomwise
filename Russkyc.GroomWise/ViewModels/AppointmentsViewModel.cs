// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class AppointmentsViewModel : ViewModelBase, IAppointmentsViewModel
{
    private readonly IAddAppointmentsViewFactory _addAppointmentsViewFactory;
    private readonly IAppointmentsRepository _appointmentsRepository;
    private readonly ILogger _logger;

    [ObservableProperty]
    private AppointmentsCollection _appointments;

    public AppointmentsViewModel(
        IAppointmentsRepository appointmentsRepository,
        IAddAppointmentsViewFactory addAppointmentsViewFactory,
        ILogger logger
    )
    {
        _appointmentsRepository = appointmentsRepository;
        _addAppointmentsViewFactory = addAppointmentsViewFactory;
        _logger = logger;

        Appointments = new AppointmentsCollection();

        GetAppointments();
    }

    void GetAppointments()
    {
        var _cancellationTokenSource = new CancellationTokenSource();
        Task.Run(
            async () =>
            {
                while (!_cancellationTokenSource.IsCancellationRequested)
                {
                    var command = new SynchronizeCollectionCommand<
                        Appointment,
                        AppointmentsCollection
                    >(ref _appointments, _appointmentsRepository.GetCollection());
                    command.Execute();
                    OnPropertyChanged();
                    _logger.Log(this, "Synchronized appointments collection.");
                    await Task.Delay(2000, _cancellationTokenSource.Token);
                }
            },
            _cancellationTokenSource.Token
        );
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
