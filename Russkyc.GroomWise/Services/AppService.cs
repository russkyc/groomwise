// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services
{
    public class AppService : IAppService
    {
        public ObservableCollection<INavItem> NavItems { get; set; }

        private IAppService _appServiceImplementation;

        public AppService()
        {
            ConfigureNavItems();
        }

        void ConfigureNavItems()
        {
            NavItems = new ObservableCollection<INavItem>(new []
            {
                new NavItem
                {
                    Name = "Dashboard",
                    Page = "DashboardView",
                    Icon = new MaterialIcon
                    {
                        Kind = MaterialIconKind.ViewDashboard
                    },
                    Selected = true
                },
                new NavItem
                {
                    Name = "Appointments",
                    Page = "AppointmentsView",
                    Icon = new MaterialIcon
                    {
                        Kind = MaterialIconKind.EventAvailable
                    },
                },
                new NavItem
                {
                    Name = "Services",
                    Page = "ServicesView",
                    Icon = new MaterialIcon
                    {
                        Kind = MaterialIconKind.BubbleChart
                    },
                },
                new NavItem
                {
                    Name = "Customers",
                    Page = "CustomersView",
                    Icon = new MaterialIcon
                    {
                        Kind = MaterialIconKind.People
                    },
                },
                new NavItem
                {
                    Name = "Pets",
                    Page = "PetsView",
                    Icon = new MaterialIcon
                    {
                        Kind = MaterialIconKind.Pets
                    },
                },
                new NavItem
                {
                    Name = "Reports",
                    Page = "ReportsView",
                    Icon = new MaterialIcon
                    {
                        Kind = MaterialIconKind.BarChart
                    },
                }
            });
        }

    }
}