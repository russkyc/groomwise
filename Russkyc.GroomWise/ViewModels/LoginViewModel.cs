// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class LoginViewModel : ViewModelBase, ILoginViewModel
{
    private readonly ISessionService _sessionService;
    private readonly IAccountsRepositoryService _accountsRepositoryService;
    
    [ObservableProperty]
    private IApplicationService _applicationService;
    
    [ObservableProperty]
    private ObservableCollection<INotification> _notifications;

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
        ISessionService sessionService, IApplicationService applicationService)
    {
        _accountsRepositoryService = accountsRepositoryService;
        _sessionService = sessionService;
        ApplicationService = applicationService;
        Notifications = new ObservableCollection<INotification>();
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
            var notification = new Notification();
            var account = _accountsRepositoryService.Get(
                account => account.Username == Username.SHA256());
            if (account != null)
            {
                if (account.Password == Password.SHA256())
                {
                    RemoveNotification();
                    _sessionService.Login(account);
                    ApplicationService.BuildNavItems();
                    BuilderServices.Resolve<MainView>().Show();
                    BuilderServices.Resolve<LoginView>().Hide();
                    BuilderServices.Resolve<IDashboardViewModel>().Invalidate();
                }
                else
                {
                    
                    BuilderServices.Resolve<LoginView>().ClearPasswords();
                    notification.Description = "Password is incorrect.";
                    notification.Type = NotificationType.Danger;
                    
                    if (Notifications.Count > 0)
                    {
                        Notifications[0] = notification;
                    }
                    else
                    {
                        Notifications.Add(notification);
                    }
                }
            }
            else
            {
                
                BuilderServices.Resolve<LoginView>().ClearFields();
                notification.Description = "Account does not exist.";
                notification.Type = NotificationType.Danger;
                
                if (Notifications.Count > 0)
                {
                    Notifications[0] = notification;
                }
                else
                {
                    Notifications.Add(notification);
                }
            }
        }
    }

    [RelayCommand]
    private void RemoveNotification()
    {
        Notifications.Clear();
    }
}