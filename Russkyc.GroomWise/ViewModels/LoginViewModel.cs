// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class LoginViewModel : ViewModelBase, ILoginViewModel
{
    private readonly IAccountsRepository _accountsRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IHotkeyListenerService _hotkeyListenerService;
    private readonly ISessionManagerService _sessionManagerService;
    private readonly IEncryptionService _encryptionService;
    private readonly ILogger _logger;

    [ObservableProperty]
    private IApplicationService _applicationService;

    [ObservableProperty]
    private NotificationsCollection _notifications;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Password cannot be blank.")]
    private string? _password;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Username cannot be blank.")]
    [RegularExpression("^[a-zA-Z0-9_-]{1,16}$", ErrorMessage = "Invalid Username format.")]
    private string? _username;

    public LoginViewModel(
        ISessionManagerService sessionManagerService,
        IHotkeyListenerService hotkeyListenerService,
        IAccountsRepository accountsRepository,
        IEmployeeRepository employeeRepository,
        IApplicationService applicationService,
        IEncryptionService encryptionService,
        ILogger logger
    )
    {
        _accountsRepository = accountsRepository;
        _employeeRepository = employeeRepository;
        _sessionManagerService = sessionManagerService;
        ApplicationService = applicationService;
        _encryptionService = encryptionService;
        _logger = logger;
        _hotkeyListenerService = hotkeyListenerService;

        Notifications = new NotificationsCollection();
    }

    [RelayCommand]
    private void SwitchFocus(object element)
    {
        element.GetType().GetMethod("Focus")?.Invoke(element, null);
    }

    [RelayCommand]
    private void Login()
    {
        // Force data validation of all fields
        ValidateAllProperties();

        // Dont Continue if field validation has errors
        if (HasErrors)
            return;

        var hashedPassword = Password.SHA256();
        var encryptedUsername = _encryptionService.Encrypt(Username!);

        var account = _accountsRepository.Get(a => a.Username == encryptedUsername);

        if (account == null)
        {
            BuilderServices.Resolve<ILoginView>().ClearFields();
            ShowNotification("Account does not exist.", NotificationType.Danger);
            _logger.Log(this, "Unsuccessful login attempt, Account does not exist");
            return;
        }

        if (hashedPassword != account.Password)
        {
            BuilderServices.Resolve<ILoginView>().ClearFields("Password");
            ShowNotification("Password is incorrect.", NotificationType.Danger);
            _logger.Log(this, "Unsuccessful login attempt, Wrong Username or Password");
            return;
        }

        var employee = _employeeRepository.Get(e => e.Id == account.EmployeeId);

        if (employee == null)
        {
            ShowNotification(
                "Account does not match any employee record.",
                NotificationType.Danger
            );
            _logger.Log(
                this,
                "Unsuccessful login attempt, Account does not match any employee record"
            );
            return;
        }

        _sessionManagerService.StartSession(employee);
        ApplicationService.BuildNavItems();

        Task.Run(async () =>
        {
            RegisterHotKeys();
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                RemoveNotification();
                BuilderServices.Resolve<IMainView>().Show();
                BuilderServices.Resolve<ILoginView>().Hide();
            });
        });

        void ShowNotification(string description, NotificationType type)
        {
            var notification = new Notification { Description = description, Type = type };
            if (Notifications.Count > 0)
                Notifications[0] = notification;
            else
                Notifications.Add(notification);
        }
    }

    [RelayCommand]
    private void RemoveNotification()
    {
        Notifications.Clear();
    }

    [RelayCommand]
    void RegisterHotKeys()
    {
        _hotkeyListenerService.RegisterHotkey(
            new Hotkey()
                .WithModifier(Modifier.Control)
                .WithModifier(Modifier.Alt)
                .WithKey(Key.A)
                .WithName("CreateAppointment"),
            () =>
            {
                Application.Current.Dispatcher.BeginInvoke(() =>
                {
                    BuilderServices.Resolve<IAddAppointmentsViewFactory>().Create().Show();
                });
            }
        );
    }
}
