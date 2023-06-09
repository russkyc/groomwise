// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Collections;

public static class NavItemsCollection
{
    public static IEnumerable<NavItem> Get()
    {
        yield return new NavItem
        {
            Name = "Dashboard",
            Page = typeof(IDashboardView),
            Icon = new MaterialIcon { Kind = MaterialIconKind.ViewDashboard },
            Selected = true,
            Roles = new[] { 1, 2, 3 }
        };
        yield return new NavItem
        {
            Name = "Appointments",
            Page = typeof(IAppointmentsView),
            Icon = new MaterialIcon { Kind = MaterialIconKind.EventAvailable },
            Selected = false,
            Roles = new[] { 1, 2, 3 }
        };
        yield return new NavItem
        {
            Name = "Services",
            Page = typeof(IServicesView),
            Icon = new MaterialIcon { Kind = MaterialIconKind.BubbleChart },
            Selected = false,
            Roles = new[] { 1, 2 }
        };
        yield return new NavItem
        {
            Name = "Pets",
            Page = typeof(IPetsView),
            Icon = new MaterialIcon { Kind = MaterialIconKind.Pets },
            Selected = false,
            Roles = new[] { 2, 3 }
        };
        yield return new NavItem
        {
            Name = "Customers",
            Page = typeof(ICustomersView),
            Icon = new MaterialIcon { Kind = MaterialIconKind.People },
            Selected = false,
            Roles = new[] { 2, 3 }
        };
        yield return new NavItem
        {
            Name = "Employees",
            Page = typeof(IEmployeesView),
            Icon = new MaterialIcon { Kind = MaterialIconKind.AccountGroup },
            Selected = false,
            Roles = new[] { 2 }
        };
        yield return new NavItem
        {
            Name = "Inventory",
            Page = typeof(IInventoryView),
            Icon = new MaterialIcon { Kind = MaterialIconKind.Table },
            Selected = false,
            Roles = new[] { 2, 3 }
        };
        yield return new NavItem
        {
            Name = "Reports",
            Page = typeof(IReportsView),
            Icon = new MaterialIcon { Kind = MaterialIconKind.BarChart },
            Selected = false,
            Roles = new[] { 1, 2 }
        };
    }
}
