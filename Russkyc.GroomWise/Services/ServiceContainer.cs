// Copyright (C) 2023 Russell Camo (Russkyc). - All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.


namespace Russkyc.GroomWise.Services;

public static class ServiceContainer
{
    public static IServicesContainer ConfigureServices()
    {
        return new ServicesCollection()
            
            // Add Application Services
            .AddSingleton<IAppService, AppService>()
            .AddSingleton<IThemeManagerService, ThemeManagerManagerService>()
            
            // Add Factory Services
            .AddSingleton<IPetFactoryService, PetFactoryService>()
            .AddSingleton<IGroomerFactoryService, GroomerFactoryService>()
            .AddSingleton<ICustomerFactoryService, CustomerFactoryService>()
            .AddSingleton<IAppointmentFactoryService, AppointmentFactoryService>()
            
            // Add Data Services
            .AddSingleton<IDatabaseService, LiteDbDataService>()
            .AddSingleton<IAppointmentsRepository, AppointmentsRepository>()
            
            // Add ViewModels
            .AddTransient<IAppointmentsViewModel, AppointmentsViewModel>()
            .AddTransient<ICustomersViewModel, CustomersViewModel>()
            .AddTransient<IDashboardViewModel, DashboardViewModel>()
            .AddTransient<IPetsViewModel, PetsViewModel>()
            .AddTransient<IReportsViewModel, ReportsViewModel>()
            .AddTransient<IServicesViewModel, ServicesViewModel>()
            .AddTransient<IMainViewModel, MainViewModel>()
            
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