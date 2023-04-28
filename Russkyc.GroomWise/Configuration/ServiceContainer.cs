// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.



namespace Russkyc.GroomWise.Configuration;

public class ServiceContainer
{
    public static IServicesContainer ConfigureServices()
    {
        return new ServicesCollection()
            .AddSingleton<IAppInstance, AppInstance>()
            .AddSingleton<IMainViewModel, MainViewModel>()
            .AddSingleton<AppointmentsView>(nameof(AppointmentsView))
            .AddSingleton<CustomersView>(nameof(CustomersView))
            .AddSingleton<DashboardView>(nameof(DashboardView))
            .AddSingleton<PetsView>(nameof(PetsView))
            .AddSingleton<ReportsView>(nameof(ReportsView))
            .AddSingleton<ServicesView>(nameof(ServicesView))
            .AddTransient<MainView>()
            .Build();
    }
}