// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System.Collections.ObjectModel;
using Russkyc.GroomWise.Models.Interfaces;

namespace Russkyc.GroomWise.Models.Entities
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
                    Selected = true
                },
                new NavItem
                {
                    Name = "Appointments",
                    Page = "AppointmentsView"
                },
                new NavItem
                {
                    Name = "Services",
                    Page = "ServicesView"
                },
                new NavItem
                {
                    Name = "Customers",
                    Page = "CustomersView"
                },
                new NavItem
                {
                    Name = "Pets",
                    Page = "PetsView"
                },
                new NavItem
                {
                    Name = "Reports",
                    Page = "ReportsView"
                }
            });
        }

    }
}