// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class AddAppointmentsViewModel : ViewModelBase, IAddAppointmentsViewModel
{
    private readonly ILogger _logger;
    private readonly IDialogFactory _dialogFactory;
    private readonly IAppointmentFactory _appointmentFactory;

    private readonly CustomerRepository _customerRepository;
    private readonly AppointmentsRepository _appointmentsRepository;
    private readonly GroomingServiceRepository _groomingServiceRepository;

    [ObservableProperty]
    private CustomersCollection _customers;

    [ObservableProperty]
    private GroomingServiceCollection _groomigServices;

    [ObservableProperty]
    private ObservableCollection<string> _months;

    [ObservableProperty]
    private ObservableCollection<int> _days;

    [ObservableProperty]
    private ObservableCollection<string> _times;

    [ObservableProperty]
    private ObservableCollection<string> _timeOfDay;

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private DateTime? _date;

    public AddAppointmentsViewModel(
        ILogger logger,
        IDialogFactory dialogFactory,
        IAppointmentFactory appointmentFactory,
        CustomerRepository customerRepository,
        AppointmentsRepository appointmentsRepository,
        GroomingServiceRepository groomingServiceRepository
    )
    {
        _logger = logger;
        _dialogFactory = dialogFactory;
        _customerRepository = customerRepository;
        _appointmentFactory = appointmentFactory;
        _appointmentsRepository = appointmentsRepository;
        _groomingServiceRepository = groomingServiceRepository;

        Customers = new CustomersCollection();
        GroomigServices = new GroomingServiceCollection();

        TimeOfDay = new ObservableCollection<string>();
        Months = new ObservableCollection<string>();
        Times = new ObservableCollection<string>();
        Days = new ObservableCollection<int>();

        GetServices();
        GetSchedules();
    }

    void GetServices()
    {
        var command = new SynchronizeCollectionCommand<GroomingService, GroomingServiceCollection>(
            ref _groomigServices,
            _groomingServiceRepository.GetAll().ToList()
        );
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

            int hour = 7;
            int minute = 00;
            while (hour < 11)
            {
                if (minute == 00)
                {
                    Times.Add($"{hour}:{minute:00}");
                    minute = 30;
                }
                if (minute == 30)
                {
                    Times.Add($"{hour}:{minute:00}");
                    hour++;
                    minute = 0;
                }
            }
            TimeOfDay.Add("AM");
            TimeOfDay.Add("PM");
        });
    }

    [RelayCommand]
    void SetTime(string time)
    {
        var timeData = time.Split(":");
        var hour = int.Parse(timeData[0]);
        var minute = int.Parse(timeData[1]);
        Date = new DateTime(Date!.Value.Year, Date.Value.Month, Date.Value.Day, hour, minute, 0);
    }

    [RelayCommand]
    void SetTimeOfDay(string timeOfDay)
    {
        if (timeOfDay.Equals("PM"))
        {
            Date = new DateTime(
                Date.Value.Year,
                Date.Value.Month,
                Date.Value.Day,
                Date.Value.Hour + 12,
                Date.Value.Minute,
                0
            );
        }
    }

    [RelayCommand]
    void AddAppointment()
    {
        var appointment = _appointmentFactory.Create(appointment =>
        {
            appointment.Date = Date;
            appointment.Title = Title;
            appointment.Description = Description;
        });
        _appointmentsRepository.Add(appointment);
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
    }
}
