// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.App;

public partial class ApplicationService : ObservableObject, IApplicationService
{
    private readonly IConfigProvider _configProvider;
    private readonly ISessionManagerService _sessionManagerService;

    private readonly List<NavItem> _nav;

    public string? AppAuthor { get; set; }
    public string? AppVersion { get; set; }

    [ObservableProperty]
    private SynchronizedObservableCollection<NavItem> _navItems;

    public ApplicationService(
        ISessionManagerService sessionManagerService,
        IConfigProvider configProvider,
        MaterialIconFactory materialIconFactory,
        NavItemFactory navItemFactory,
        RoleRepository roleRepository
    )
    {
        _sessionManagerService = sessionManagerService;
        _configProvider = configProvider;

        var roles = roleRepository.GetAll();
        _nav = new List<NavItem>
        {
            navItemFactory.Create(navItem =>
            {
                navItem.Name = "Dashboard";
                navItem.Page = typeof(IDashboardView);
                navItem.Icon = materialIconFactory.Create(
                    icon => icon.Kind = MaterialIconKind.ViewDashboard
                );
                navItem.Selected = true;
                navItem.Roles = roles
                    .Where(role => role.Id == 1 || role.Id == 2 || role.Id == 3)
                    .ToArray();
                navItem.Selected = true;
            }),
            navItemFactory.Create(navItem =>
            {
                navItem.Name = "Appointments";
                navItem.Page = typeof(IAppointmentsView);
                navItem.Icon = materialIconFactory.Create(
                    icon => icon.Kind = MaterialIconKind.EventAvailable
                );
                navItem.Selected = false;
                navItem.Roles = roles
                    .Where(role => role.Id == 1 || role.Id == 2 || role.Id == 3)
                    .ToArray();
            }),
            navItemFactory.Create(navItem =>
            {
                navItem.Name = "Services";
                navItem.Page = typeof(IServicesView);
                navItem.Icon = materialIconFactory.Create(
                    icon => icon.Kind = MaterialIconKind.BubbleChart
                );
                navItem.Selected = false;
                navItem.Roles = roles.Where(role => role.Id == 1 || role.Id == 2).ToArray();
            }),
            navItemFactory.Create(navItem =>
            {
                navItem.Name = "Pets";
                navItem.Page = typeof(IPetsView);
                navItem.Icon = materialIconFactory.Create(
                    icon => icon.Kind = MaterialIconKind.Pets
                );
                navItem.Selected = false;
                navItem.Roles = roles.Where(role => role.Id == 2 || role.Id == 3).ToArray();
            }),
            navItemFactory.Create(navItem =>
            {
                navItem.Name = "Customers";
                navItem.Page = typeof(ICustomersView);
                navItem.Icon = materialIconFactory.Create(
                    icon => icon.Kind = MaterialIconKind.People
                );
                navItem.Selected = false;
                navItem.Roles = roles.Where(role => role.Id == 2 || role.Id == 3).ToArray();
            }),
            navItemFactory.Create(navItem =>
            {
                navItem.Name = "Employees";
                navItem.Page = typeof(IEmployeesView);
                navItem.Icon = materialIconFactory.Create(
                    icon => icon.Kind = MaterialIconKind.AccountGroup
                );
                navItem.Selected = false;
                navItem.Roles = roles.Where(role => role.Id == 2).ToArray();
            }),
            navItemFactory.Create(navItem =>
            {
                navItem.Name = "Inventory";
                navItem.Page = typeof(IInventoryView);
                navItem.Icon = materialIconFactory.Create(
                    icon => icon.Kind = MaterialIconKind.Table
                );
                navItem.Selected = false;
                navItem.Roles = roles.Where(role => role.Id == 2 || role.Id == 3).ToArray();
            }),
            navItemFactory.Create(navItem =>
            {
                navItem.Name = "Reports";
                navItem.Page = typeof(IReportsView);
                navItem.Icon = materialIconFactory.Create(
                    icon => icon.Kind = MaterialIconKind.BarChart
                );
                navItem.Selected = false;
                navItem.Roles = roles.Where(role => role.Id == 1 || role.Id == 2).ToArray();
            })
        };
        NavItems = new SynchronizedObservableCollection<NavItem>();
        BuildAppInfo();
    }

    public void BuildAppInfo()
    {
        AppVersion = _configProvider.Version;
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
                    _nav.Where(
                            navItem => navItem.Roles!.Any(navItemRole => navItemRole.Id == role.Id)
                        )
                        .ToList()
                )
        );
    }
}
