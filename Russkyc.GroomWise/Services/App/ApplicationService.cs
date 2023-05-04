// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.App;

public partial class ApplicationService : ObservableObject, IApplicationService
{

    private readonly IConfigurationService _configurationService;
    private readonly ISessionManagerService _sessionManagerService;
    private readonly INavItemFactoryService _navItemFactoryService;
    private readonly IMaterialIconFactory _materialIconFactory;
    
    [ObservableProperty]
    private string? _appAuthor;

    [ObservableProperty]
    private string? _appVersion;
    
    [ObservableProperty]
    private NavItemsCollection _navItems;

    public ApplicationService(
        ISessionManagerService sessionManagerService,
        IConfigurationService configurationService,
        INavItemFactoryService navItemFactoryService,
        IMaterialIconFactory materialIconFactory)
    {
        _sessionManagerService = sessionManagerService;
        _configurationService = configurationService;
        _navItemFactoryService = navItemFactoryService;
        _materialIconFactory = materialIconFactory;
        NavItems = new NavItemsCollection();
        BuildAppInfo();
    }

    public void BuildAppInfo()
    {
        AppAuthor = "Russell Camo (@russkyc)";
        AppVersion = _configurationService.Config.ReadString("AppSettings", "Version");
    }
    
    public void BuildNavItems()
    {
        NavItems.Clear();
        foreach (INavItem navItem in new[]
                 {
                     _navItemFactoryService.Create("Dashboard",
                         typeof(IDashboardView),
                         _materialIconFactory.Create(MaterialIconKind.ViewDashboard),
                         true,
                         new[]
                         {
                             AccountType.Admin,
                             AccountType.Groomer,
                             AccountType.Manager,
                             AccountType.Customer
                         }
                     ),
                     _navItemFactoryService.Create("Appointments",
                         typeof(IAppointmentsView),
                         _materialIconFactory.Create(MaterialIconKind.EventAvailable),
                         false,
                         new[]
                         {
                             AccountType.Admin,
                             AccountType.Groomer,
                             AccountType.Manager,
                             AccountType.Customer
                         }
                     ),
                     _navItemFactoryService.Create("Services",
                         typeof(IServicesView),
                         _materialIconFactory.Create(MaterialIconKind.BubbleChart),
                         false,
                         new[]
                         {
                             AccountType.Admin,
                             AccountType.Manager
                         }
                     ),
                     _navItemFactoryService.Create("Customers",
                         typeof(ICustomersView),
                         _materialIconFactory.Create(MaterialIconKind.People),
                         false,
                         new[]
                         {
                             AccountType.Admin,
                             AccountType.Manager
                         }
                     ),
                     _navItemFactoryService.Create("Pets",
                         typeof(IPetsView),
                         _materialIconFactory.Create(MaterialIconKind.BubbleChart),
                         false,
                         new[]
                         {
                             AccountType.Admin,
                             AccountType.Groomer,
                             AccountType.Manager,
                             AccountType.Customer
                         }
                     ),
                     _navItemFactoryService.Create("Reports",
                         typeof(IReportsView),
                         _materialIconFactory.Create(MaterialIconKind.BarChart),
                         false,
                         new[]
                         {
                             AccountType.Admin,
                             AccountType.Manager
                         }
                     )
                 })
            
            // Checks if nav item restriction allows access to current session type
            if (_sessionManagerService != null
                && _sessionManagerService.SessionUser?.Type != null
                && navItem.AccountTypes!
                    .ToList()
                    .Contains((AccountType)_sessionManagerService.SessionUser?
                        .Type!))
                // Add nav item
                NavItems.Add(navItem);
    }
}