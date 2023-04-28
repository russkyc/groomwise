// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.



namespace Russkyc.GroomWise.Services;

public class ServiceContainer
{
    public static IServicesContainer ConfigureServices()
    {
        return new ServicesCollection()
            
            // Add Services
            .AddSingleton<IAppService, AppService>()
            
            // Add ViewModels
            .AddSingleton<IAppointmentsViewModel, AppointmentsViewModel>()
            .AddSingleton<ICustomersViewModel, CustomersViewModel>()
            .AddSingleton<IDashboardViewModel, DashboardViewModel>()
            .AddSingleton<IPetsViewModel, PetsViewModel>()
            .AddSingleton<IReportsViewModel, ReportsViewModel>()
            .AddSingleton<IServicesViewModel, ServicesViewModel>()
            .AddSingleton<IMainViewModel, MainViewModel>()
            
            // Add Views
            .AddSingleton<AppointmentsView>(nameof(AppointmentsView))
            .AddSingleton<CustomersView>(nameof(CustomersView))
            .AddSingleton<DashboardView>(nameof(DashboardView))
            .AddSingleton<PetsView>(nameof(PetsView))
            .AddSingleton<ReportsView>(nameof(ReportsView))
            .AddSingleton<ServicesView>(nameof(ServicesView))
            .AddSingleton<MainView>()
            
            // Build Container
            .Build();
    }
}