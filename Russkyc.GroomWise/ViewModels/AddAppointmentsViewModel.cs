// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class AddAppointmentsViewModel : ViewModelBase, IAddAppointmentsViewModel
{
    private readonly ILogger _logger;
    private readonly DialogFactory _dialogFactory;
    private readonly AppointmentFactory _appointmentFactory;

    private readonly CustomerRepository _customerRepository;
    private readonly AppointmentRepository _appointmentRepository;
    private readonly GroomingServiceRepository _groomingServiceRepository;

    [ObservableProperty]
    private SynchronizedObservableCollection<Customer> _customers;

    [ObservableProperty]
    private SynchronizedObservableCollection<GroomingService> _services;

    [ObservableProperty]
    private ObservableCollection<string> _months;

    [ObservableProperty]
    private ObservableCollection<int> _days;

    [ObservableProperty]
    private ObservableCollection<KeyValuePair<string, TimeInfo>> _times;

    [ObservableProperty]
    private string? _title;

    [ObservableProperty]
    private string? _description;

    [ObservableProperty]
    private DateTime _date;

    [ObservableProperty]
    private TimeInfo? _time;

    [ObservableProperty]
    private bool _isAm;

    public AddAppointmentsViewModel(
        ILogger logger,
        DialogFactory dialogFactory,
        AppointmentFactory appointmentFactory,
        CustomerRepository customerRepository,
        AppointmentRepository appointmentRepository,
        GroomingServiceRepository groomingServiceRepository
    )
    {
        _logger = logger;
        _dialogFactory = dialogFactory;
        _customerRepository = customerRepository;
        _appointmentFactory = appointmentFactory;
        _appointmentRepository = appointmentRepository;
        _groomingServiceRepository = groomingServiceRepository;

        Customers = new SynchronizedObservableCollection<Customer>();
        Services = new SynchronizedObservableCollection<GroomingService>();

        Months = new ObservableCollection<string>();
        Times = new ObservableCollection<KeyValuePair<string, TimeInfo>>();
        Days = new ObservableCollection<int>();

        GetServices();
        GetSchedules();
    }

    void GetServices()
    {
        var command = new SynchronizeCollectionCommand<
            GroomingService,
            SynchronizedObservableCollection<GroomingService>
        >(ref _services, _groomingServiceRepository.GetAll().ToList());
        command.Execute();
    }

    void GetSchedules()
    {
        Task.Run(() =>
        {
            int month = DateTime.Now.Month;
            int advanceMonth = 4;
            while (advanceMonth > 0)
            {
                DateTime monthDate = new DateTime(2023, month, 1);
                Months.Add(monthDate.ToString("MMMM"));
                month = month == 12 ? 1 : month + 1;
                advanceMonth--;
            }
            for (int i = 1; i <= 31; i++)
            {
                Days.Add(i);
            }

            int hour = 1;
            int minute = 00;
            while (hour < 11)
            {
                if (minute == 00)
                {
                    Times.Add(
                        new KeyValuePair<string, TimeInfo>(
                            $"{hour:00}:{minute:00}",
                            new TimeInfo
                            {
                                Time = $"{hour:00}:{minute:00}",
                                AmHour = hour,
                                PmHour = hour + 12,
                                Minutes = minute,
                                IsAm = true
                            }
                        )
                    );
                    minute = 30;
                }
                if (minute == 30)
                {
                    Times.Add(
                        new KeyValuePair<string, TimeInfo>(
                            $"{hour:00}:{minute:00}",
                            new TimeInfo
                            {
                                Time = $"{hour:00}:{minute:00}",
                                AmHour = hour,
                                PmHour = hour + 12,
                                Minutes = minute,
                                IsAm = true
                            }
                        )
                    );
                    hour++;
                    minute = 0;
                }
            }

            Time = Times[0].Value;
        });
    }

    void ClearFields()
    {
        Title = string.Empty;
        Description = string.Empty;
    }

    [RelayCommand]
    void AddAppointment()
    {
        var appointment = _appointmentFactory.Create(appointment =>
        {
            appointment.Date = new DateTime(
                Date.Year,
                Date.Month,
                Date.Day,
                IsAm ? Time!.AmHour : Time!.PmHour,
                Time.Minutes,
                0
            );
            appointment.Title = Title;
            appointment.Description = Description;
        });
        _appointmentRepository.Add(appointment);
        _logger.Log(this, $"Scheduled {appointment.Title} on {appointment.Date:f}");
        BuilderServices.Resolve<IAddAppointmentsView>().Hide();
        _dialogFactory
            .Create(dialog =>
            {
                dialog.MessageBoxText = "Appointment Saved";
                dialog.Caption = $"{appointment.Title} has been added to appointments";
            })
            .AsChild()
            .ShowDialog();
        ClearFields();
    }
}
