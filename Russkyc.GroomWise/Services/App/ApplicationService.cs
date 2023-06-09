// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.App;

public partial class ApplicationService : ObservableObject, IApplicationService
{
    private readonly IConfigProvider _configProvider;
    private readonly ISessionManagerService _sessionManagerService;

    [ObservableProperty]
    private SynchronizedObservableCollection<NavItem> _navItems;

    public ApplicationService(
        ISessionManagerService sessionManagerService,
        IConfigProvider configProvider
    )
    {
        _sessionManagerService = sessionManagerService;
        _configProvider = configProvider;

        NavItems = new SynchronizedObservableCollection<NavItem>();
    }

    public void BuildNavItems()
    {
        // Check for session
        var session = _sessionManagerService.Session;
        if (session == null)
            return;

        // Check for role
        var role = session.SessionRole;
        if (role == null)
            return;

        // Build Nav Items
        Application.Current.Dispatcher.BeginInvoke(() => NavItems.Clear());
        Application.Current.Dispatcher.BeginInvoke(
            () =>
                NavItems.AddRange(
                    NavItemsCollection
                        .Get()
                        .Where(navItem => navItem.Roles!.Any(navItemRole => navItemRole == role.Id))
                        .ToList()
                )
        );
    }
}
