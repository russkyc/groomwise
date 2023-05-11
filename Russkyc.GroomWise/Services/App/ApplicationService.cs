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
            _navItemFactory.Create(
                "Dashboard",
                typeof(IDashboardView),
                _materialIconFactory.Create(MaterialIconKind.ViewDashboard),
                true,
                new[] { EmployeeType.Admin, EmployeeType.Groomer, EmployeeType.Manager }
            ),
            _navItemFactory.Create(
                "Appointments",
                typeof(IAppointmentsView),
                _materialIconFactory.Create(MaterialIconKind.EventAvailable),
                false,
                new[] { EmployeeType.Admin, EmployeeType.Groomer, EmployeeType.Manager }
            ),
            _navItemFactory.Create(
                "Services",
                typeof(IServicesView),
                _materialIconFactory.Create(MaterialIconKind.BubbleChart),
                false,
                new[] { EmployeeType.Admin, EmployeeType.Manager }
            ),
            _navItemFactory.Create(
                "Pets",
                typeof(IPetsView),
                _materialIconFactory.Create(MaterialIconKind.Pets),
                false,
                new[] { EmployeeType.Admin, EmployeeType.Groomer, EmployeeType.Manager }
            ),
            _navItemFactory.Create(
                "Customers",
                typeof(ICustomersView),
                _materialIconFactory.Create(MaterialIconKind.People),
                false,
                new[] { EmployeeType.Admin, EmployeeType.Manager }
            ),
            _navItemFactory.Create(
                "Employees",
                typeof(IEmployeesView),
                _materialIconFactory.Create(MaterialIconKind.PeopleGroup),
                false,
                new[] { EmployeeType.Manager }
            ),
            _navItemFactory.Create(
                "Reports",
                typeof(IReportsView),
                _materialIconFactory.Create(MaterialIconKind.BarChart),
                false,
                new[] { EmployeeType.Admin, EmployeeType.Manager }
            )
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
