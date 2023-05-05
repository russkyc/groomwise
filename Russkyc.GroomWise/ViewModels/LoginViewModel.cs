// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class LoginViewModel : ViewModelBase, ILoginViewModel
{
    private readonly IAccountsRepositoryService _accountsRepositoryService;
    private readonly ISessionManagerService _sessionManagerService;
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
        IAccountsRepositoryService accountsRepositoryService,
        ISessionManagerService sessionManagerService, IApplicationService applicationService, ILogger logger)
    {
        _accountsRepositoryService = accountsRepositoryService;
        _sessionManagerService = sessionManagerService;
        ApplicationService = applicationService;
        _logger = logger;
        
        Notifications = new NotificationsCollection();
    }

    [RelayCommand]
    private void SwitchFocus(object element)
    {
        element.GetType()
            .GetMethod("Focus")?
            .Invoke(element, null);
    }

    [RelayCommand]
    private void Login()
    {
        ValidateAllProperties();
        if (!HasErrors)
        {
            Notification notification = new Notification();
            Account? account = _accountsRepositoryService.Get(
                account => account.Username == Username.SHA256());
            if (account != null)
            {
                if (account.Password == Password.SHA256())
                {
                    _sessionManagerService.StartSession(account);
                    ApplicationService.BuildNavItems();
                    
                    RemoveNotification();
                    
                    BuilderServices.Resolve<IMainView>()
                        .Show();
                    BuilderServices.Resolve<ILoginView>()
                        .Hide();
                    
                    _logger.Log(this, $"Login successful for user {account.Username?.Substring(0,12)}({account.Type})");
                }
                else
                {
                    BuilderServices.Resolve<ILoginView>()
                        .ClearFields("Password");
                    
                    notification.Description = "Password is incorrect.";
                    notification.Type = NotificationType.Danger;

                    if (Notifications.Count > 0)
                        Notifications[0] = notification;
                    else
                        Notifications.Add(notification);
                    _logger.Log(this, $"Unsuccessful login attempt from user {account.Username?.Substring(0,12)}(Wrong credentials)");
                }
            }
            else
            {

                BuilderServices.Resolve<ILoginView>().ClearFields();
                
                notification.Description = "Account does not exist.";
                notification.Type = NotificationType.Danger;

                if (Notifications.Count > 0)
                    Notifications[0] = notification;
                else
                    Notifications.Add(notification);
                _logger.Log(this, $"Unsuccessful login attempt from user(No account)");
            }
        }
    }

    [RelayCommand]
    private void RemoveNotification()
    {
        Notifications.Clear();
    }
}