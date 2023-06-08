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

    private readonly SessionFactory _sessionFactory;
    private readonly RoleRepository _roleRepository;
    private readonly AccountRepository _accountRepository;
    private readonly EmployeeRepository _employeeRepository;
    private readonly EmployeeRoleRepository _employeeRoleRepository;
    private readonly EmployeeAccountRepository _employeeAccountRepository;

    [ObservableProperty]
    private IApplicationService _applicationService;

    [ObservableProperty]
    private SynchronizedObservableCollection<Notification> _notifications;

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
        ILogger logger,
        ISessionManagerService sessionManagerService,
        EmployeeAccountRepository employeeAccountRepository,
        EmployeeRepository employeeRepository,
        IApplicationService applicationService,
        IEncryptionService encryptionService,
        AccountRepository accountRepository,
        EmployeeRoleRepository employeeRoleRepository,
        RoleRepository roleRepository,
        SessionFactory sessionFactory
    )
    {
        _logger = logger;
        _employeeRepository = employeeRepository;
        _sessionManagerService = sessionManagerService;
        ApplicationService = applicationService;
        _encryptionService = encryptionService;
        _accountRepository = accountRepository;
        _employeeRoleRepository = employeeRoleRepository;
        _roleRepository = roleRepository;
        _sessionFactory = sessionFactory;
        _employeeAccountRepository = employeeAccountRepository;

        Notifications = new SynchronizedObservableCollection<Notification>();
    }

    [RelayCommand]
    private void Login()
    {
        // Force data validation of all fields
        ValidateAllProperties();

        // Check if validation has errors
        if (HasErrors)
            return;

        var account = _accountRepository.Find(
            a => a.Username == _encryptionService.Encrypt(Username!)
        );

        // Check if account is null
        if (account is null)
        {
            BuilderServices.Resolve<ILoginView>().ClearFields();
            ShowNotification("Account does not exist", NotificationType.Danger);
            _logger.Log(this, "unsuccessful login attempt, account does not exist");
            return;
        }

        // Check if account password is equal to input password
        if (_encryptionService.Hash(Password!) != account.Password)
        {
            BuilderServices.Resolve<ILoginView>().ClearFields("Password");
            ShowNotification("Password is incorrect", NotificationType.Danger);
            _logger.Log(this, "Unsuccessful login attempt, wrong password");
            return;
        }

        var employeeAccount = _employeeAccountRepository.Find(e => e.AccountId == account.Id);

        // Check if employee account is null
        if (employeeAccount == null)
        {
            ShowNotification("Account does not match any employee record", NotificationType.Danger);
            _logger.Log(
                this,
                "Unsuccessful login attempt, account does not match any employee record"
            );
            return;
        }

        var employee = _employeeRepository.Find(
            employee => employee.Id == employeeAccount.EmployeeId
        );

        // Check if employee is null
        if (employee == null)
        {
            ShowNotification("Employee does not exist", NotificationType.Danger);
            _logger.Log(this, "Unsuccessful login attempt, employee does not exist");
            return;
        }

        var employeeRole = _employeeRoleRepository.Find(
            employeeRole => employeeRole.EmployeeId == employee.Id
        );
        if (employeeRole == null)
        {
            _logger.Log(this, "Employee is not assigned any role");
            return;
        }

        var role = _roleRepository.Find(role => role.Id == employeeRole.RoleId);

        // Check if role is null
        if (role == null)
        {
            _logger.Log(this, "Role is non-existent");
            return;
        }

        var session = _sessionFactory.Create(session =>
        {
            session.SessionUser = employee;
            session.SessionRole = role;
        });

        _sessionManagerService.StartSession(session);
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
    }

    [RelayCommand]
    void RemoveNotification()
    {
        Notifications.Clear();
    }

    void ShowNotification(string description, NotificationType type)
    {
        var notification = new Notification { Description = description, Type = type };
        if (Notifications.Count > 0)
            Notifications[0] = notification;
        else
            Notifications.Add(notification);
    }
}
