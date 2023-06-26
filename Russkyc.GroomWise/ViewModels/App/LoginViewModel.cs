// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels.App
{
    public partial class LoginViewModel : ViewModelBase, ILoginViewModel
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;
        private readonly IConfigProvider _configProvider;
        private readonly IEncryptionService _encryptionService;
        private readonly IApplicationService _applicationService;
        private readonly ISessionManagerService _sessionManagerService;

        private readonly IFactory<Session> _sessionFactory;

        public string AppVersion => _configProvider.Version;

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
            IDbContext dbContext,
            ISessionManagerService sessionManagerService,
            IApplicationService applicationService,
            IEncryptionService encryptionService,
            IConfigProvider configProvider,
            IFactory<Session> sessionFactory
        )
        {
            _logger = logger;
            _dbContext = dbContext;
            _sessionManagerService = sessionManagerService;
            _applicationService = applicationService;
            _encryptionService = encryptionService;
            _sessionFactory = sessionFactory;
            _configProvider = configProvider;

            Notifications = new SynchronizedObservableCollection<Notification>();
        }

        [RelayCommand]
        private void Login()
        {
            ValidateAllProperties();

            if (HasErrors)
                return;

            var encryptedUsername = _encryptionService.Encrypt(Username!);
            var account = _dbContext.AccountsRepository.Find(a => a.Username == encryptedUsername);

            if (account is null)
                HandleNonexistentAccount();
            else
                AuthenticateUser(account);
        }

        private void HandleNonexistentAccount()
        {
            ClearFields();
            ShowNotification("Account does not exist", NotificationType.Danger);
            _logger.Log(this, "Unsuccessful login attempt, account does not exist");
        }

        private void ClearFields(string field = "")
        {
            BuilderServices.Resolve<ILoginView>().ClearFields(field);
        }

        private void AuthenticateUser(Account account)
        {
            if (_encryptionService.Hash(Password!) != account.Password)
                HandleIncorrectPassword();
            else
                CreateSession(account);
        }

        private void HandleIncorrectPassword()
        {
            ClearFields("Password");
            ShowNotification("Password is incorrect", NotificationType.Danger);
            _logger.Log(this, "Unsuccessful login attempt, wrong password");
        }

        private void CreateSession(Account account)
        {
            var employeeAccount = _dbContext.EmployeeAccountRepository.Find(
                e => e.AccountId == account.Id
            );

            if (employeeAccount != null)
            {
                var employee = _dbContext.EmployeeRepository.Find(
                    e => e.Id == employeeAccount.EmployeeId
                );

                if (employee != null)
                {
                    var employeeRole = _dbContext.EmployeeRoleRepository.Find(
                        er => er.EmployeeId == employee.Id
                    );

                    if (employeeRole != null)
                        StartSession(employee, employeeRole);
                    else
                        _logger.Log(this, "Employee is not assigned any role");
                }
                else
                {
                    ShowNotification("Employee does not exist", NotificationType.Danger);
                    _logger.Log(this, "Unsuccessful login attempt, employee does not exist");
                }
            }
            else
            {
                ShowNotification(
                    "Account does not match any employee record",
                    NotificationType.Danger
                );
                _logger.Log(
                    this,
                    "Unsuccessful login attempt, account does not match any employee record"
                );
            }
        }

        private void StartSession(Employee employee, EmployeeRole employeeRole)
        {
            var role = _dbContext.RoleRepository.Find(er => er.Id == employeeRole.RoleId);

            if (role != null)
            {
                var session = _sessionFactory.Create(s =>
                {
                    s.SessionUser = employee;
                    s.SessionRole = role;
                });

                _sessionManagerService.StartSession(session);
                _applicationService.BuildNavItems();

                RemoveNotification();
                ToggleViews();
            }
            else
            {
                _logger.Log(this, "Role is non-existent");
            }
        }

        private void ToggleViews()
        {
            BuilderServices.Resolve<IMainView>().Show();
            BuilderServices.Resolve<ILoginView>().Hide();
        }

        [RelayCommand]
        void RemoveNotification()
        {
            Notifications.Clear();
        }

        void ShowNotification(string description, NotificationType type)
        {
            var notification = new Notification { Description = description, Type = type };
            if (Notifications.Any())
                Notifications[0] = notification;
            else
                Notifications.Add(notification);
        }
    }
}
