// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.App;

public partial class ApplicationService : ObservableObject, IApplicationService
{
    private readonly IConfigProvider _configProvider;
    private readonly ISessionManagerService _sessionManagerService;

    private List<INavItem> _nav;

    [ObservableProperty]
    private string? _appAuthor;

    [ObservableProperty]
    private string? _appVersion;

    [ObservableProperty]
    private NavItemsCollection _navItems;

    public ApplicationService(
        ISessionManagerService sessionManagerService,
        IConfigProvider configProvider,
        IMaterialIconFactory materialIconFactory,
        INavItemFactory navItemFactory
    )
    {
        _sessionManagerService = sessionManagerService;
        _configProvider = configProvider;

        _nav = new List<INavItem>
        {
            navItemFactory.Create(navItem =>
            {
                navItem.Name = "Dashboard";
                navItem.Page = typeof(IDashboardView);
                navItem.Icon = materialIconFactory.Create(
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
            navItemFactory.Create(navItem =>
            {
                navItem.Name = "Appointments";
                navItem.Page = typeof(IAppointmentsView);
                navItem.Icon = materialIconFactory.Create(
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
            navItemFactory.Create(navItem =>
            {
                navItem.Name = "Services";
                navItem.Page = typeof(IServicesView);
                navItem.Icon = materialIconFactory.Create(
                    icon => icon.Kind = MaterialIconKind.BubbleChart
                );
                navItem.Selected = false;
                navItem.AccountTypes = new[] { EmployeeType.Admin, EmployeeType.Manager };
            }),
            navItemFactory.Create(navItem =>
            {
                navItem.Name = "Pets";
                navItem.Page = typeof(IPetsView);
                navItem.Icon = materialIconFactory.Create(
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
            navItemFactory.Create(navItem =>
            {
                navItem.Name = "Customers";
                navItem.Page = typeof(ICustomersView);
                navItem.Icon = materialIconFactory.Create(
                    icon => icon.Kind = MaterialIconKind.People
                );
                navItem.Selected = false;
                navItem.AccountTypes = new[] { EmployeeType.Admin, EmployeeType.Manager };
            }),
            navItemFactory.Create(navItem =>
            {
                navItem.Name = "Employees";
                navItem.Page = typeof(IEmployeesView);
                navItem.Icon = materialIconFactory.Create(
                    icon => icon.Kind = MaterialIconKind.AccountGroup
                );
                navItem.Selected = false;
                navItem.AccountTypes = new[] { EmployeeType.Manager };
            }),
            navItemFactory.Create(navItem =>
            {
                navItem.Name = "Inventory";
                navItem.Page = typeof(IInventoryView);
                navItem.Icon = materialIconFactory.Create(
                    icon => icon.Kind = MaterialIconKind.Table
                );
                navItem.Selected = false;
                navItem.AccountTypes = new[] { EmployeeType.Groomer, EmployeeType.Manager };
            }),
            navItemFactory.Create(navItem =>
            {
                navItem.Name = "Reports";
                navItem.Page = typeof(IReportsView);
                navItem.Icon = materialIconFactory.Create(
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
        AppVersion = _configProvider.Version;
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
