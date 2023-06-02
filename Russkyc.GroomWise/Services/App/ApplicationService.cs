// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.App;

public partial class ApplicationService : ObservableObject, IApplicationService
{
    private readonly IMaterialIconFactory _materialIconFactory;
    private readonly IConfigurationService _configurationService;
    private readonly ISessionManagerService _sessionManagerService;
    private readonly INavItemFactory _navItemFactory;

    private List<INavItem> _nav;

    [ObservableProperty]
    private string? _appAuthor;

    [ObservableProperty]
    private string? _appVersion;

    [ObservableProperty]
    private NavItemsCollection _navItems;

    public ApplicationService(
        ISessionManagerService sessionManagerService,
        IConfigurationService configurationService,
        IMaterialIconFactory materialIconFactory,
        INavItemFactory navItemFactory
    )
    {
        _sessionManagerService = sessionManagerService;
        _configurationService = configurationService;
        _materialIconFactory = materialIconFactory;
        _navItemFactory = navItemFactory;

        _nav = new List<INavItem>
        {
            _navItemFactory.Create(navItem =>
            {
                navItem.Name = "Dashboard";
                navItem.Page = typeof(IDashboardView);
                navItem.Icon = _materialIconFactory.Create(
                    icon => icon.Kind = MaterialIconKind.ViewDashboard
                );
                navItem.Selected = true;
                navItem.AccountTypes = new[]
                {
                    EmployeeType.Admin,
                    EmployeeType.Groomer,
                    EmployeeType.Manager
                };
            }),
            _navItemFactory.Create(navItem =>
            {
                navItem.Name = "Appointments";
                navItem.Page = typeof(IAppointmentsView);
                navItem.Icon = _materialIconFactory.Create(
                    icon => icon.Kind = MaterialIconKind.EventAvailable
                );
                navItem.Selected = false;
                navItem.AccountTypes = new[]
                {
                    EmployeeType.Admin,
                    EmployeeType.Groomer,
                    EmployeeType.Manager
                };
            }),
            _navItemFactory.Create(navItem =>
            {
                navItem.Name = "Services";
                navItem.Page = typeof(IServicesView);
                navItem.Icon = _materialIconFactory.Create(
                    icon => icon.Kind = MaterialIconKind.BubbleChart
                );
                navItem.Selected = false;
                navItem.AccountTypes = new[] { EmployeeType.Admin, EmployeeType.Manager };
            }),
            _navItemFactory.Create(navItem =>
            {
                navItem.Name = "Pets";
                navItem.Page = typeof(IPetsView);
                navItem.Icon = _materialIconFactory.Create(
                    icon => icon.Kind = MaterialIconKind.Pets
                );
                navItem.Selected = false;
                navItem.AccountTypes = new[]
                {
                    EmployeeType.Admin,
                    EmployeeType.Groomer,
                    EmployeeType.Manager
                };
            }),
            _navItemFactory.Create(navItem =>
            {
                navItem.Name = "Customers";
                navItem.Page = typeof(ICustomersView);
                navItem.Icon = _materialIconFactory.Create(
                    icon => icon.Kind = MaterialIconKind.People
                );
                navItem.Selected = false;
                navItem.AccountTypes = new[] { EmployeeType.Admin, EmployeeType.Manager };
            }),
            _navItemFactory.Create(navItem =>
            {
                navItem.Name = "Employees";
                navItem.Page = typeof(IEmployeesView);
                navItem.Icon = _materialIconFactory.Create(
                    icon => icon.Kind = MaterialIconKind.PeopleGroup
                );
                navItem.Selected = false;
                navItem.AccountTypes = new[] { EmployeeType.Manager };
            }),
            _navItemFactory.Create(navItem =>
            {
                navItem.Name = "Reports";
                navItem.Page = typeof(IReportsView);
                navItem.Icon = _materialIconFactory.Create(
                    icon => icon.Kind = MaterialIconKind.BarChart
                );
                navItem.Selected = false;
                navItem.AccountTypes = new[] { EmployeeType.Admin, EmployeeType.Manager };
            })
        };
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
        Task.Run(async () =>
        {
            var currentUserType = (EmployeeType)_sessionManagerService.SessionUser!.EmployeeType!;
            await Application.Current.Dispatcher.InvokeAsync(() => NavItems.Clear());

            _nav.Where(navItem => navItem.AccountTypes!.Contains(currentUserType))
                .ToList()
                .ForEach(async navItem =>
                {
                    navItem.Selected = navItem is { Name: "Dashboard" };
                    await Application.Current.Dispatcher.InvokeAsync(() => NavItems.Add(navItem));
                });
        });
    }
}
