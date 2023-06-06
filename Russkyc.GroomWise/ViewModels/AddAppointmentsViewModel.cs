// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class AddAppointmentsViewModel : ViewModelBase, IAddAppointmentsViewModel
{
    private readonly IDialogFactory _dialogFactory;
    private readonly IAppointmentFactory _appointmentFactory;
    private readonly IAppointmentsViewModel _appointmentsViewModel;

    private readonly Repository<Customer> _customerRepository;
    private readonly Repository<Appointment> _appointmentsRepository;
    private readonly Repository<GroomingService> _groomingServiceRepository;

    [ObservableProperty]
    private Appointment _newAppointment;

    [ObservableProperty]
    private CustomersCollection _customers;

    [ObservableProperty]
    private GroomingServiceCollection _groomigServices;

    public AddAppointmentsViewModel(
        IDialogFactory dialogFactory,
        IAppointmentFactory appointmentFactory,
        IAppointmentsViewModel appointmentsViewModel,
        Repository<Customer> customerRepository,
        Repository<Appointment> appointmentsRepository,
        Repository<GroomingService> groomingServiceRepository
    )
    {
        _dialogFactory = dialogFactory;
        _customerRepository = customerRepository;
        _appointmentFactory = appointmentFactory;
        _appointmentsViewModel = appointmentsViewModel;
        _appointmentsRepository = appointmentsRepository;
        _groomingServiceRepository = groomingServiceRepository;

        Customers = new CustomersCollection();
        GroomigServices = new GroomingServiceCollection();

        NewAppointment = appointmentFactory.Create();

        GetServices();
    }

    void GetServices()
    {
        Task.Run(() =>
        {
            var command = new SynchronizeCollectionCommand<
                GroomingService,
                GroomingServiceCollection
            >(ref _groomigServices, _groomingServiceRepository.GetAll().ToList());
            command.Execute();
        });
    }

    [RelayCommand]
    void AddAppointment()
    {
        if (
            _appointmentsRepository.Find(appointment => appointment.Date == NewAppointment.Date)
            is null
        )
        {
            _appointmentsRepository.Add(NewAppointment);
            _dialogFactory
                .Create(dialog =>
                {
                    dialog.MessageBoxText = "Appointment Saved";
                    dialog.Caption = $"{NewAppointment.Title} has been added to appointments";
                })
                .AsChild()
                .ShowDialog();
            _appointmentsViewModel.GetAppointmentsCommand.Execute(null);
            BuilderServices.Resolve<IAddAppointmentsView>().Hide();
            NewAppointment = _appointmentFactory.Create();
        }
    }
}
