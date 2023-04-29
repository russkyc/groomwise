// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.ViewModels;

public partial class DashboardViewModel : ViewModelBase, IDashboardViewModel
{
    private readonly INotificationFactoryService _notificationFactoryService;

    private object _collectionsLock;
    
    [ObservableProperty]
    private ObservableCollection<INotification> _notifications;

    public DashboardViewModel(INotificationFactoryService notificationFactoryService)
    {
        _notificationFactoryService = notificationFactoryService;
        _collectionsLock = new object();
        Notifications = new ObservableCollection<INotification>();
        BindingOperations.EnableCollectionSynchronization(Notifications,_collectionsLock);
        GetNotifications();
    }

    void GetNotifications()
    {
        new Thread(
            () =>
            {
                for (int i = 0; i < 20; i++)
                {
                    var item = _notificationFactoryService.Create();
                    item.Title = $"Notification {i}";
                    item.Description = $"Insert description for notification {i}";
                    Notifications.Add(item);
                    Thread.Sleep(2000);
                }
            }).Start();
    }
}