// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class LoginViewModel : ViewModelBase, ILoginViewModel
{
    private readonly ILogger _logger;
    private readonly IEncryptionService _encryptionService;
    private readonly ISessionManagerService _sessionManagerService;

    private readonly AccountsRepository _accountsRepository;
    private readonly EmployeeRepository _employeeRepository;

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
        AccountsRepository accountsRepository,
        EmployeeRepository employeeRepository,
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

        var account = _accountsRepository.Find(
            a => a.Username == _encryptionService.Encrypt(Username!)
        );

        if (account == null)
        {
            BuilderServices.Resolve<ILoginView>().ClearFields();
            ShowNotification("Account does not exist.", NotificationType.Danger);
            _logger.Log(this, "Unsuccessful login attempt, Account does not exist");
            return;
        }

        if (_encryptionService.Hash(Password!) != account.Password)
        {
            BuilderServices.Resolve<ILoginView>().ClearFields("Password");
            ShowNotification("Password is incorrect.", NotificationType.Danger);
            _logger.Log(this, "Unsuccessful login attempt, Wrong Username or Password");
            return;
        }

        var employee = new Employee(); // _employeeRepository.Find(e => e.Id == account.EmployeeId);

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
            await DispatchHelper.UiInvokeAsync(() =>
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
}
