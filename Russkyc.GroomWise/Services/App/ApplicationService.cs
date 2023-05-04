// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.App;

public partial class ApplicationService : ObservableObject, IApplicationService
{
    private readonly ISessionService _sessionService;

    [ObservableProperty] private ObservableCollection<INavItem> _navItems;

    public ApplicationService(ISessionService sessionService)
    {
        NavItems = new ObservableCollection<INavItem>();
        _sessionService = sessionService;
    }

    public void BuildNavItems()
    {
        NavItems.Clear();
        foreach (NavItem navItem in new[]
                 {
                     new NavItem
                     {
                         Name = "Dashboard",
                         Page = "DashboardView",
                         Icon = new MaterialIcon
                         {
                             Kind = MaterialIconKind.ViewDashboard
                         },
                         Selected = true,
                         AccountTypes = new[]
                         {
                             AccountType.Admin,
                             AccountType.Groomer,
                             AccountType.Manager,
                             AccountType.Customer
                         }
                     },
                     new NavItem
                     {
                         Name = "Appointments",
                         Page = "AppointmentsView",
                         Icon = new MaterialIcon
                         {
                             Kind = MaterialIconKind.EventAvailable
                         },
                         AccountTypes = new[]
                         {
                             AccountType.Admin,
                             AccountType.Groomer,
                             AccountType.Manager,
                             AccountType.Customer
                         }
                     },
                     new NavItem
                     {
                         Name = "Services",
                         Page = "ServicesView",
                         Icon = new MaterialIcon
                         {
                             Kind = MaterialIconKind.BubbleChart
                         },
                         AccountTypes = new[]
                         {
                             AccountType.Admin,
                             AccountType.Manager
                         }
                     },
                     new NavItem
                     {
                         Name = "Customers",
                         Page = "CustomersView",
                         Icon = new MaterialIcon
                         {
                             Kind = MaterialIconKind.People
                         },
                         AccountTypes = new[]
                         {
                             AccountType.Admin,
                             AccountType.Manager
                         }
                     },
                     new NavItem
                     {
                         Name = "Pets",
                         Page = "PetsView",
                         Icon = new MaterialIcon
                         {
                             Kind = MaterialIconKind.Pets
                         },
                         AccountTypes = new[]
                         {
                             AccountType.Admin,
                             AccountType.Groomer,
                             AccountType.Manager,
                             AccountType.Customer
                         }
                     },
                     new NavItem
                     {
                         Name = "Reports",
                         Page = "ReportsView",
                         Icon = new MaterialIcon
                         {
                             Kind = MaterialIconKind.BarChart
                         },
                         AccountTypes = new[]
                         {
                             AccountType.Admin,
                             AccountType.Manager
                         }
                     }
                 })
        {
            if (_sessionService != null && _sessionService.GetSession()?.Type != null && navItem.AccountTypes!
                    .ToList().Contains((AccountType)_sessionService.GetSession()?.Type!))
            {
                NavItems.Add(navItem);
            }
        }
    }
}