// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class DashboardViewModel : ViewModelBase, IDashboardViewModel
{
    private readonly INotificationFactoryService _notificationFactoryService;
    
    [ObservableProperty]
    private NotificationsCollection _notifications;
    
    [ObservableProperty]
    private ISessionManagerService _sessionManagerService;

    [ObservableProperty] private string? _welcomeMessage;

    public DashboardViewModel(
        ISessionManagerService sessionManagerService,
        INotificationFactoryService notificationFactoryService)
    {
        Notifications = new NotificationsCollection();
        SessionManagerService = sessionManagerService;
        _notificationFactoryService = notificationFactoryService;
    }

    public void Invalidate()
    {
        GetWelcomeMessage();
        GetNotifications();
    }

    [RelayCommand]
    private void GetNotifications()
    {
        new Thread(
            _ =>
            {
                for (var i = 0; i < 20; i++)
                {
                    Notification? item = _notificationFactoryService.Create();
                    item.Title = $"Notification {i}";
                    item.Description = $"Insert description for notification {i}";
                    Notifications.Insert(0,item);
                    Thread.Sleep(1000);
                }
            }).Start();
    }

    [RelayCommand]
    private void GetWelcomeMessage()
    {
        WelcomeMessage = $"Good Morning, {SessionManagerService.SessionUser?.FirstName}!";
    }
}