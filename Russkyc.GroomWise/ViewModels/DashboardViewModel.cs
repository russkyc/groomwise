// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class DashboardViewModel : ViewModelBase, IDashboardViewModel
{
    private readonly object _collectionsLock;
    private readonly INotificationFactoryService _notificationFactoryService;

    [ObservableProperty] private string? _welcomeMessage;
    [ObservableProperty] private ISessionService _sessionService;
    [ObservableProperty] private ObservableCollection<INotification> _notifications;

    public DashboardViewModel(
        ISessionService sessionService,
        INotificationFactoryService notificationFactoryService)
    {
        _collectionsLock = new object();
        SessionService = sessionService;
        _notificationFactoryService = notificationFactoryService;
        Notifications = new ObservableCollection<INotification>();
        BindingOperations.EnableCollectionSynchronization(_notifications, _collectionsLock);
    }

    [RelayCommand]
    private void GetNotifications()
    {
        for (var i = 0; i < 20; i++)
        {
            Notification? item = _notificationFactoryService.Create();
            item.Title = $"Notification {i}";
            item.Description = $"Insert description for notification {i}";
            Notifications.Add(item);
        }
    }

    [RelayCommand]
    private void GetWelcomeMessage()
    {
        WelcomeMessage = $"Good Morning, {SessionService.GetSession()?.FirstName}!";
    }

    public void Invalidate()
    {
        GetWelcomeMessage();
        GetNotifications();
    }
}